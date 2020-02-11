package com.example.spacecadet;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

public class Asteroid {
    int x = 0, y = 0;
    Bitmap asteroid;

    public Asteroid(int screenX, int screenY, Resources res) {
        asteroid = BitmapFactory.decodeResource(res, R.drawable.asteroid);
        asteroid = Bitmap.createScaledBitmap(asteroid, screenX, screenY, false);
    }
}
