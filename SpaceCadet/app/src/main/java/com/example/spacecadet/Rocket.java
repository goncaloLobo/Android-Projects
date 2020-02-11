package com.example.spacecadet;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

public class Rocket {

    int x = 0, y = 0;
    Bitmap rocket;

    public Rocket(int screenX, int screenY, Resources res) {
        rocket = BitmapFactory.decodeResource(res, R.drawable.rocket);
        rocket = Bitmap.createScaledBitmap(rocket, screenX, screenY, false);
    }
}
