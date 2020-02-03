package com.example.android.simplegesturesapp;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Point;
import android.os.Bundle;
import android.view.WindowManager;

public class GameActivity extends AppCompatActivity {

    private CustomView customView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // ficar em fullscreen
        //getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        Point point = new Point();
        getWindowManager().getDefaultDisplay().getSize(point);

        customView = new CustomView(this, point.x, point.y);

        setContentView(customView);
    }

    @Override
    protected void onPause() {
        super.onPause();
        customView.pause();
    }

    @Override
    protected void onResume() {
        super.onResume();
        customView.resume();
    }
}
