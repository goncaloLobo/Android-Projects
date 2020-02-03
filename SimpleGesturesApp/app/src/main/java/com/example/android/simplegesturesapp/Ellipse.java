package com.example.android.simplegesturesapp;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import static com.example.android.simplegesturesapp.CustomView.screenRatioX;
import static com.example.android.simplegesturesapp.CustomView.screenRatioY;

public class Ellipse {
    int width, height;
    int x, y;

    Bitmap ellipseBitmap1, ellipseBitmap2, ellipseBitmap3, ellipseBitmap4, ellipseBitmap5;

    public Ellipse(Resources res){

        ellipseBitmap1 = BitmapFactory.decodeResource(res, R.drawable.ellipse);
        ellipseBitmap2 = BitmapFactory.decodeResource(res, R.drawable.ellipse);
        ellipseBitmap3 = BitmapFactory.decodeResource(res, R.drawable.ellipse);
        ellipseBitmap4 = BitmapFactory.decodeResource(res, R.drawable.ellipse);

        width = ellipseBitmap1.getWidth();
        height = ellipseBitmap1.getHeight();

        width /= 6;
        height /= 6;

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);

        ellipseBitmap1 = Bitmap.createScaledBitmap(ellipseBitmap1, width, height, false);
        ellipseBitmap2 = Bitmap.createScaledBitmap(ellipseBitmap2, width, height, false);
        ellipseBitmap3 = Bitmap.createScaledBitmap(ellipseBitmap3, width, height, false);
        ellipseBitmap4 = Bitmap.createScaledBitmap(ellipseBitmap4, width, height, false);
    }

    public Rect getCollisionShape () {
        return new Rect(x, y, x + width, y + height);
    }
}
