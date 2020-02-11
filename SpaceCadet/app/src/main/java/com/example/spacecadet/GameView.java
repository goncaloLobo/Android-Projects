package com.example.spacecadet;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.view.SurfaceView;

public class GameView extends SurfaceView implements Runnable {

    private Thread thread;
    private boolean isPlaying;
    private int screenX, screenY;
    private Paint paint;
    private Asteroid[] asteroids;
    private Rocket rocket;
    public static float screenRatioX, screenRatioY;
    private Background background1, background2;

    public GameView(Context context, int screenX, int screenY) {
        super(context);

        this.screenX = screenX;
        this.screenY = screenY;
        screenRatioX = 1920f / screenX;
        screenRatioY = 1080f / screenY;

        background1 = new Background(screenX, screenY, getResources());
        background2 = new Background(screenX, screenY, getResources());

        background2.x = screenX;
        background2.y = screenY;

        paint = new Paint();
        rocket = new Rocket(this, screenX, getResources());

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

        background1.y -= 10 * screenRatioY;
        background2.y -= 10 * screenRatioY;

        if (background1.y + background1.background.getHeight() < 0) {
            background1.y = screenY;
        }

        if (background2.y + background2.background.getHeight() < 0) {
            background2.y = screenY;
        }
    }

    private void draw() {

        if (getHolder().getSurface().isValid()) {
            Canvas canvas = getHolder().lockCanvas();
            canvas.drawBitmap(background1.background, background1.x, background1.y, paint);
            canvas.drawBitmap(background2.background, background2.x, background2.y, paint);

            // desenhar os vários asteroids
            for (Asteroid ast : asteroids) {
                canvas.drawBitmap(ast.getAsteroid(), ast.x, ast.y, paint);
            }

            // coloca o foguetão no centro, em baixo no ecrã
            int startX1 = (canvas.getWidth() - rocket.getRocket().getWidth()) / 2;
            int startY1 = (canvas.getHeight() - rocket.getRocket().getHeight() - 50);
            canvas.drawBitmap(rocket.getRocket(), startX1, startY1, paint);

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
}
