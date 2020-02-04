package com.example.android.simplegesturesapp;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.os.CountDownTimer;
import android.os.Handler;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.SurfaceView;
import android.view.View;
import android.widget.Toast;

import java.util.Timer;
import java.util.TimerTask;


public class CustomView extends SurfaceView implements Runnable {

    private static final String DEBUG_TAG = "DEBUG";
    private GameActivity gameActivity;
    private int screenX, screenY, score = 0;
    public static float screenRatioX, screenRatioY;
    private Background background1;
    private boolean isPlaying, isGameOver = false;
    private Paint paint;
    private Thread thread;

    int x, y;

    private GestureDetector gestureDetector;
    private Toupeira toupeira1;
    private Toupeira[] toupeiras;
    private Hole hole1, hole2;

    public CustomView(GameActivity gameActivity, int screenX, int screenY) {
        super(gameActivity);

        this.gameActivity = gameActivity;

        this.screenX = screenX;
        this.screenY = screenY;
        screenRatioX = 1920f / screenX;
        screenRatioY = 1080f / screenY;

        background1 = new Background(screenX, screenY, getResources());

        paint = new Paint();
        paint.setTextSize(128);
        paint.setColor(Color.WHITE);

        toupeira1 = new Toupeira(getResources());
        toupeiras = new Toupeira[2];
        toupeiras[0] = toupeira1;

        hole1 = new Hole(getResources());
        hole2 = new Hole(getResources());
        gestureDetector = new GestureDetector(gameActivity.getApplicationContext(), new GestureListener());
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        /*int action = event.getAction();
        switch(action) {
            case (MotionEvent.ACTION_DOWN) :
                Log.d(DEBUG_TAG,"Action was DOWN");
                return true;
            case (MotionEvent.ACTION_MOVE) :
                Log.d(DEBUG_TAG,"Action was MOVE");
                Log.d(DEBUG_TAG, "onTouch: (x,y): (" + event.getX() + " , " + event.getY() + ")");
                return true;
            case (MotionEvent.ACTION_UP) :
                performClick();
                Log.d(DEBUG_TAG,"Action was UP");
                return true;
            case (MotionEvent.ACTION_CANCEL) :
                Log.d(DEBUG_TAG,"Action was CANCEL");
                return true;
            case (MotionEvent.ACTION_OUTSIDE) :
                Log.d(DEBUG_TAG,"Movement occurred outside bounds " +
                        "of current screen element");
                return true;
            default :
                return super.onTouchEvent(event);
        }*/
        return gestureDetector.onTouchEvent(event);
    }

    private void update() {

    }

    private void draw() {
        if (getHolder().getSurface().isValid()) {
            final Canvas canvas = getHolder().lockCanvas();

            canvas.drawBitmap(background1.background, background1.x, background1.y, paint);
            //canvas.drawBitmap(toupeira1.getToupeira(), toupeira1.x, toupeira1.y, paint);
            canvas.drawBitmap(hole1.getHole(), hole1.x + 100, hole1.y, paint);
            canvas.drawBitmap(hole2.getHole(), hole1.x + 700, hole1.y, paint);

            getHolder().unlockCanvasAndPost(canvas);
        }
    }

    private void sleep() {

    }

    @Override
    public boolean performClick() {
        super.performClick();
        return true;
    }

    @Override
    public void run() {
        while (isPlaying) {
            update();
            draw();
            sleep();
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

    private class GestureListener extends GestureDetector.SimpleOnGestureListener {
        @Override
        public boolean onDown(MotionEvent e) {
            return true;
        }

        @Override
        public boolean onDoubleTap(MotionEvent e) {
            float x = e.getX();
            float y = e.getY();
            Log.d("Double Tap", "Tapped at: (" + x + "," + y + ")");
            return true;
        }

        @Override
        public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,
                               float velocityY) {
            Log.d("onFling", "Fling: (" + velocityX + "," + velocityY + ")");
            return true;
        }

    }
}
