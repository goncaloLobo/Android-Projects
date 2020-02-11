package com.example.spacecadet;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import static com.example.spacecadet.GameView.screenRatioX;
import static com.example.spacecadet.GameView.screenRatioY;

public class Asteroid {

    public int speed = 20;
    public boolean wasShot = true;
    int x = 0, y = 0, width, height, asteroidCounter = 1;
    Bitmap asteroid;

    public Asteroid(Resources res) {
        asteroid = BitmapFactory.decodeResource(res, R.drawable.asteroid);

        width = asteroid.getWidth();
        height = asteroid.getHeight();

        //width /= 6;
        //height /= 6;

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);

        asteroid = Bitmap.createScaledBitmap(asteroid, width, height, false);
    }

    public Bitmap getAsteroid() {
        return asteroid;
    }

    public Rect getCollisionShape() {
        return new Rect(x, y, x + width, y + height);
    }
}