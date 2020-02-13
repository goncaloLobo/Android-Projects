package com.example.spacecadet;

import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Rect;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.SurfaceView;

import java.util.Random;

public class GameView extends SurfaceView implements Runnable {

    private Thread thread;
    private boolean isPlaying, onTheGround, isGameOver = false;
    private int screenX, screenY;
    private Paint paint;
    private Asteroid[] asteroids;
    private Asteroid asteroidTeste;
    private Rocket rocket;
    public static float screenRatioX, screenRatioY;

    private GestureDetector gestureDetector;
    private GameActivity gameActivity;

    // Minimal x and y axis swipe distance.
    private static int MIN_SWIPE_DISTANCE_X = 100;
    private static int MIN_SWIPE_DISTANCE_Y = 100;

    // Maximal x and y axis swipe distance.
    private static int MAX_SWIPE_DISTANCE_X = 1000;
    private static int MAX_SWIPE_DISTANCE_Y = 1000;

    private float x1, x2, y1, y2;
    private static final int MIN_DISTANCE = 150;
    private Random random;

    private Background background1;

    public GameView(GameActivity gameActivity, int screenX, int screenY) {
        super(gameActivity);
        this.gameActivity = gameActivity;

        background1 = new Background(screenX, screenY, getResources());

        this.screenX = screenX;
        this.screenY = screenY;
        screenRatioX = 1920f / screenX;
        screenRatioY = 1080f / screenY;

        paint = new Paint();
        paint.setColor(getResources().getColor(R.color.blackBackground));
        rocket = new Rocket(this, screenX, screenY, getResources());

        // criacao dos vários asteroids
        asteroidTeste = new Asteroid(getResources());
        //asteroids = new Asteroid[3];
        //for (int i = 0; i < 3; i++) {
        //    Asteroid ast = new Asteroid(getResources());
        //    asteroids[i] = ast;
        //}

        random = new Random();
    }

    @Override
    public void run() {
        while (isPlaying) {
            update();
            draw();
            sleep();
        }
    }

    private void update() {

        // movimento do foguetão para os lados
        if (rocket.isGoingLeft)
            rocket.x -= 30 * screenRatioX;
        else
            rocket.x += 30 * screenRatioX;

        if (rocket.x < 0)
            rocket.x = 0;

        if (rocket.x >= screenX - rocket.width)
            rocket.x = screenX - rocket.width;

        // movimento do foguetão para cima e para baixo
        if (rocket.isGoingUp) {
            rocket.y += (30 * screenRatioY) / 2;
        } else {
            rocket.y -= (30 * screenRatioY) / 2;
        }

        if (rocket.y < 0)
            rocket.y = 0;

        if (rocket.y >= screenY - rocket.height)
            rocket.y = screenY - rocket.height;


        // for (Asteroid ast : asteroids){

        asteroidTeste.y += asteroidTeste.speed;

        if (asteroidTeste.y + asteroidTeste.height > screenY) {
            if (!asteroidTeste.wasShot) {
                Log.d("GameOver: ", "entrei");
                isGameOver = true;
                saveIfHighScore();
                waitBeforeExiting();
                return;
            }

            int bound = (int) (30 * screenRatioY);
            asteroidTeste.speed = random.nextInt(bound);

            if (asteroidTeste.speed < 10 * screenRatioY) {
                asteroidTeste.speed = (int) (10 * screenRatioY);
            }

            asteroidTeste.y = screenY;
            asteroidTeste.x = random.nextInt(screenX - asteroidTeste.width);

            asteroidTeste.wasShot = false;

            if (Rect.intersects(asteroidTeste.getCollisionShape(), rocket.getCollisionShape())) {
                Log.d("entrei colisão: ", "entrei");
                isGameOver = true;
                return;
            }
        }
    }

    private void draw() {

        if (getHolder().getSurface().isValid()) {
            Canvas canvas = getHolder().lockCanvas();

            canvas.drawBitmap(background1.background, background1.x, background1.y, paint);
            
            //desenha os foguetoes
            canvas.drawBitmap(rocket.getRocket(), rocket.x, rocket.y, paint);

            // desenha os asteroids
            canvas.drawBitmap(asteroidTeste.getAsteroid(), asteroidTeste.x, asteroidTeste.y, paint);

            if (isGameOver) {
                isPlaying = false;
                getHolder().unlockCanvasAndPost(canvas);
                return;
            }


            getHolder().unlockCanvasAndPost(canvas);
        }
    }

    private void waitBeforeExiting() {

        try {
            Thread.sleep(3000);
            gameActivity.startActivity(new Intent(gameActivity, MainActivity.class));
            gameActivity.finish();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

    }

    private void saveIfHighScore() {
        /*
        if (prefs.getInt("highscore", 0) < score) {
            SharedPreferences.Editor editor = prefs.edit();
            editor.putInt("highscore", score);
            editor.apply();
        }
        */
    }

    private void sleep() {
        try {
            Thread.sleep(17);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void resume() {
        isPlaying = true;
        thread = new Thread(this);
        thread.start();
    }

    public void pause() {
        try {
            isPlaying = false;
            thread.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {

        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                x1 = event.getX();
                y1 = event.getY();
                performClick();
                break;
            case MotionEvent.ACTION_UP:
                x2 = event.getX();
                y2 = event.getY();
                float deltaX = x2 - x1;
                float deltaY = y2 - y1;

                if (Math.abs(deltaX) > MIN_DISTANCE) {
                    if (x1 > x2) { // Swipe esquerda para a direita
                        rocket.isGoingLeft = true;
                    } else { //Swipe direita para a esquerda
                        rocket.isGoingLeft = false;
                        rocket.isGoingRight = true;
                    }
                }
                if (Math.abs(deltaY) > MIN_DISTANCE) {
                    if (y2 > y1) { // Swipe de baixo para cima
                        rocket.isGoingUp = true;
                    } else { // Swipe de cima para baixo
                        rocket.isGoingUp = false;
                        rocket.isGoingDown = true;
                    }
                } else {
                    // consider as something else - a screen tap for example
                }
                break;
        }
        return true;
    }

    @Override
    public boolean performClick() {
        super.performClick();

        return true;
    }
}
