package com.example.android.simplegesturesapp;

import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceView;
import android.view.View;

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

        View myView = findViewById(R.id.play);
        /*myView.setOnTouchListener(new View.OnTouchListener() {
            private GestureDetector gestureDetector = new GestureDetector(getContext(), new GestureDetector.SimpleOnGestureListener() {
                @Override
                public boolean onDoubleTap(MotionEvent e) {
                    Toast.makeText(getContext(), "onDoubleTap", Toast.LENGTH_SHORT).show();
                    Log.d(DEBUG_TAG,"onDoubleTap " + " x: " + x + "; y: " + y);
                    return super.onDoubleTap(e);
                }

                @Override
                public boolean onSingleTapConfirmed(MotionEvent event) {
                    Toast.makeText(getContext(), "onSingleTapConfirmed", Toast.LENGTH_SHORT).show();
                    return false;
                }

                @Override
                public boolean onFling(MotionEvent event1, MotionEvent event2,
                                       float velocityX, float velocityY) {
                    Toast.makeText(getContext(), "onFling", Toast.LENGTH_SHORT).show();
                    return true;
                }
            });

            @Override
            public boolean onTouch(View v, MotionEvent event) {
                x = (int) event.getX();
                y = (int) event.getY();
                gestureDetector.onTouchEvent(event);
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                        Log.d(DEBUG_TAG,"Action was DOWN");
                        break;
                    case MotionEvent.ACTION_UP:
                        Log.d(DEBUG_TAG,"Action was UP");
                        break;
                    case MotionEvent.ACTION_MOVE :
                        Log.d(DEBUG_TAG,"Action was MOVE");
                        break;
                }
                return true;
            }
        });*/
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        super.onTouchEvent(event);

        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                Log.d(DEBUG_TAG, "Action was DOWN");
                break;
            case MotionEvent.ACTION_UP:
                performClick();
                break;
            case MotionEvent.ACTION_MOVE:
                Log.d(DEBUG_TAG, "Action was MOVE");
                break;
        }
        return false;
    }

    private void update() {

    }

    private void draw() {
        Log.d("ENTREI1", "entrei1");
        if (getHolder().getSurface().isValid()) {
            Log.d("ENTREI2", "entrei2");
            Canvas canvas = getHolder().lockCanvas();
            canvas.drawBitmap(background1.background, background1.x, background1.y, paint);

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
        try{
            isPlaying = false;
            thread.join();
        } catch (InterruptedException e){
            e.printStackTrace();
        }
    }
}
