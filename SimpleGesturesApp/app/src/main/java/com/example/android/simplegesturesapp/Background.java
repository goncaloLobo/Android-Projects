package com.example.android.simplegesturesapp;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

public class Background {

    int x = 0, y = 0;
    Bitmap background;

    public Background(int screenX, int screenY, Resources res){

        background = BitmapFactory.decodeResource(res, R.drawable.brown);
        background = Bitmap.createScaledBitmap(background, screenX, screenY, false);
    }
}
