package com.example.android.getwhacked;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.widget.ImageView;

import androidx.core.view.GestureDetectorCompat;

public class MainActivity extends Activity {

    private boolean isTouch = false;
    private long pressTime = -1l;
    private long releaseTime = 1l;
    private long duration = -1l;

    private ImageView play, hole1, hole2;

    private static final String DEBUG_TAG = "Gestures";
    private GestureDetectorCompat mDetector;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        play = findViewById(R.id.imageView);
        hole1 = findViewById(R.id.hole1);

        mDetector = new GestureDetectorCompat(this, new MyGestureListener());

        //play.setOnClickListener(new DoubleClickListener() {
        //    @Override
        //    public void onSingleClick(View v) {
        //        Log.d("DoubleClickListener:_", "OnSingleClick");
        //    }

        //    @Override
        //    public void onDoubleClick(View v) {
        //        Log.d("DoubleClickListener:_", "onDoubleClick");
        //    }
        //});

        //hole1.setOnClickListener(new DoubleClickListener() {
        //    @Override
        //    public void onSingleClick(View v) {
        //        Log.d("DoubleClickListener -> Hole1: ", "OnSingleClick");
        //    }

        //    @Override
        //    public void onDoubleClick(View v) {
        //        Log.d("DoubleClickListener: -> Hole1: ", "onDoubleClick");
        //        int x = Math.round(hole1.getLeft());
        //        int y = Math.round(hole1.getTop());

        //        int width = Math.round(hole1.getWidth());
        //        int height = Math.round(hole1.getHeight());
        //
        //    }
        //});
    }

    //@Override
    //public boolean onTouchEvent(MotionEvent event) {
    //    int X = (int) event.getX();
    //    int Y = (int) event.getY();

    //    int eventaction = event.getAction();
    //    switch (eventaction) {

    //        case MotionEvent.ACTION_DOWN:
    //            isTouch = true;
    //            pressTime = System.currentTimeMillis();
    //            if (releaseTime != -1l) duration = pressTime - releaseTime;
    //            Log.d("ACTION_DOWN AT COORDS:_", "X: \" + X + \" Y: \" + Y" + "pressTime: " + pressTime);
    //            break;

    //        case MotionEvent.ACTION_MOVE:
    //            Log.d("MOVE:_", "X: " + X + " Y: " + Y);
    //            break;

    //        case MotionEvent.ACTION_UP:
    //            releaseTime = System.currentTimeMillis();
    //            duration = System.currentTimeMillis() - pressTime;
    //            Log.d("ACTION_UP:_", "X: " + X + " Y: " + Y + "releaseTime: " + releaseTime);
    //            break;
    //    }
    //    return true;
    //}

    //public abstract class DoubleClickListener implements View.OnClickListener {

    //    private static final long DOUBLE_CLICK_TIME_DELTA = 300;//milliseconds
    //    long lastClickTime = 0;

    //    @Override
    //    public void onClick(View v) {
    //        long clickTime = System.currentTimeMillis();
    //        if (clickTime - lastClickTime < DOUBLE_CLICK_TIME_DELTA){
    //            onDoubleClick(v);
    //        } else {
    //            onSingleClick(v);
    //        }
    //        lastClickTime = clickTime;
    //    }

    //    public abstract void onSingleClick(View v);
    //    public abstract void onDoubleClick(View v);
    //}

    @Override
    public boolean onTouchEvent(MotionEvent event){
        this.mDetector.onTouchEvent(event);
        return super.onTouchEvent(event);
    }

    private class MyGestureListener extends GestureDetector.SimpleOnGestureListener {
        private static final String DEBUG_TAG = "Gestures";

        @Override
        public boolean onDown(MotionEvent event) {
            Log.d(DEBUG_TAG, "onDown: " + event.toString());
            return true;
        }

        @Override
        public boolean onFling(MotionEvent event1, MotionEvent event2,
                               float velocityX, float velocityY) {
            Log.d(DEBUG_TAG, "onFling: " + event1.toString() + event2.toString());
            return true;
        }

        @Override
        public boolean onDoubleTap(MotionEvent event) {
            Log.d(DEBUG_TAG, "onDoubleTap: " + event.toString());
            return true;
        }

        @Override
        public boolean onSingleTapConfirmed(MotionEvent event) {
            Log.d(DEBUG_TAG, "onSingleTapConfirmed: " + event.toString());
            return true;
        }
    }
}