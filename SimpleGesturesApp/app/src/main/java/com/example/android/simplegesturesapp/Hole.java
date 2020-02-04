package com.example.android.simplegesturesapp;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import static com.example.android.simplegesturesapp.CustomView.screenRatioX;
import static com.example.android.simplegesturesapp.CustomView.screenRatioY;

public class Hole {
    int width, height;
    int x, y;

    Bitmap hole1;
    Bitmap hole2;

    public Hole(Resources res){

        hole1 = BitmapFactory.decodeResource(res, R.drawable.hole);
        hole2 = BitmapFactory.decodeResource(res, R.drawable.hole);

        width = hole1.getWidth();
        height = hole1.getHeight();

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);
        hole1 = Bitmap.createScaledBitmap(hole1, width, height+100, false);
        hole2 = Bitmap.createScaledBitmap(hole1, width, height+100, false);
    }

    public Bitmap getHole(){
        return hole1;
    }

    public Rect getCollisionShape() {
        return new Rect(x, y, x + width, y + height);
    }
}
