package com.example.android.simplegesturesapp;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import static com.example.android.simplegesturesapp.CustomView.screenRatioX;
import static com.example.android.simplegesturesapp.CustomView.screenRatioY;

public class Toupeira {

    int width, height;
    int x, y;

    Bitmap toupeira1;

    public Toupeira(Resources res) {

        toupeira1 = BitmapFactory.decodeResource(res, R.drawable.mole1);
        width = toupeira1.getWidth();
        height = toupeira1.getHeight();

        width /= 14;
        height /= 6;

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);
        toupeira1 = Bitmap.createScaledBitmap(toupeira1, width, height, false);

    }

    public Bitmap getToupeira(){
        return toupeira1;
    }

    public Rect getCollisionShape() {
        return new Rect(x, y, x + width, y + height);
    }

}
