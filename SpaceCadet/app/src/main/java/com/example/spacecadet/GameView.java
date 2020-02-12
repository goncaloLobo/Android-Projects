package com.example.spacecadet;

import android.graphics.Canvas;
import android.graphics.Paint;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.SurfaceView;
import android.widget.Toast;

public class GameView extends SurfaceView implements Runnable {

    private Thread thread;
    private boolean isPlaying, onTheGround;
    private int screenX, screenY;
    private Paint paint;
    private Asteroid[] asteroids;
    private Rocket rocket;
    public static float screenRatioX, screenRatioY;
    private Background background1, background2;

    private GestureDetector gestureDetector;
    private GameActivity gameActivity;

    // Minimal x and y axis swipe distance.
    private static int MIN_SWIPE_DISTANCE_X = 100;
    private static int MIN_SWIPE_DISTANCE_Y = 100;

    // Maximal x and y axis swipe distance.
    private static int MAX_SWIPE_DISTANCE_X = 1000;
    private static int MAX_SWIPE_DISTANCE_Y = 1000;

    private float x1, x2, y1, y2;
    static final int MIN_DISTANCE = 150;

    public GameView(GameActivity gameActivity, int screenX, int screenY) {
        super(gameActivity);
        this.gameActivity = gameActivity;

        this.screenX = screenX;
        this.screenY = screenY;
        screenRatioX = 1920f / screenX;
        screenRatioY = 1080f / screenY;

        background1 = new Background(screenX, screenY, getResources());
        background2 = new Background(screenX, screenY, getResources());

        background2.x = screenX;
        background2.y = screenY;

        paint = new Paint();
        paint.setColor(getResources().getColor(R.color.blackBackground));
        rocket = new Rocket(this, screenX, screenY, getResources());

        // criacao dos vários asteroids
        asteroids = new Asteroid[3];
        for (int i = 0; i < 3; i++) {
            Asteroid ast = new Asteroid(getResources());
            asteroids[i] = ast;
        }
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

        // movimento do background
        background1.y -= 10 * screenRatioY;
        background2.y -= 10 * screenRatioY;

        if (background1.y + background1.background.getHeight() < 0) {
            background1.y = screenY;
        }

        if (background2.y + background2.background.getHeight() < 0) {
            background2.y = screenY;
        }

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
            Log.d("GOING UP -> ", "upppppp");
            rocket.y += (30 * screenRatioY) / 2;
        } else {
            rocket.y -= (30 * screenRatioY) / 2;
        }

        if (rocket.y < 0)
            rocket.y = 0;

        //if (rocket.y >= screenY - rocket.height)
        //    rocket.y = screenY - rocket.height;
    }

    private void draw() {

        if (getHolder().getSurface().isValid()) {
            Canvas canvas = getHolder().lockCanvas();
            //canvas.drawBitmap(background1.background, background1.x, background1.y, paint);
            //canvas.drawBitmap(background2.background, background2.x, background2.y, paint);

            // resolve a questão de continuamente repetir frames do canvas
            canvas.drawColor(getResources().getColor(R.color.blackBackground));

            // desenhar os vários asteroids
            //for (Asteroid ast : asteroids) {
            //    canvas.drawBitmap(ast.getAsteroid(), ast.x, ast.y, paint);
            //}
            //canvas.drawBitmap(asteroids[0].getAsteroid(), asteroids[0].x, asteroids[0].y, paint);

            canvas.drawBitmap(rocket.getRocket(), rocket.x, rocket.y, paint);

            getHolder().unlockCanvasAndPost(canvas);
        }
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
                break;
            case MotionEvent.ACTION_UP:
                x2 = event.getX();
                y2 = event.getY();
                float deltaX = x2 - x1;
                float deltaY = y2 - y1;

                if (Math.abs(deltaX) > MIN_DISTANCE) {
                    if (x1 > x2) { // Swipe esquerda para a direita
                        rocket.isGoingLeft = true;
                        Toast.makeText(getContext(), "Left to Right swipe [Next]", Toast.LENGTH_SHORT).show();
                    } else { //Swipe direita para a esquerda
                        rocket.isGoingLeft = false;
                        rocket.isGoingRight = true;
                        Toast.makeText(getContext(), "Right to Left swipe [Previous]", Toast.LENGTH_SHORT).show();
                    }
                }
                if (Math.abs(deltaY) > MIN_DISTANCE) {
                    if (y2 > y1) { // Swipe de baixo para cima
                        rocket.isGoingUp = true;
                        Toast.makeText(getContext(), "Down to Up swipe", Toast.LENGTH_SHORT).show();
                    } else { // Swipe de cima para baixo
                        rocket.isGoingUp = false;
                        rocket.isGoingDown = true;
                        Toast.makeText(getContext(), "Up to Down swipe", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    // consider as something else - a screen tap for example
                }
                break;
        }
        return true;
    }
}
