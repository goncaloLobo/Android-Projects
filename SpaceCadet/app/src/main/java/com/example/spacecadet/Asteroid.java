package com.example.spacecadet;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;

import static com.example.spacecadet.GameView.screenRatioX;
import static com.example.spacecadet.GameView.screenRatioY;

public class Asteroid {

    public int speed = 5;
    public boolean wasShot = true;
    int x = 0, y = 0, width, height, asteroidCounter = 1;
    Bitmap asteroid1, asteroid2, asteroid3;

    public Asteroid(Resources res) {
        asteroid1 = BitmapFactory.decodeResource(res, R.drawable.asteroid1);
        asteroid2 = BitmapFactory.decodeResource(res, R.drawable.asteroid2);
        asteroid3 = BitmapFactory.decodeResource(res, R.drawable.asteroid3);

        width = asteroid1.getWidth();
        height = asteroid1.getHeight();

        width /= 5;
        height /= 2;

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);

        asteroid1 = Bitmap.createScaledBitmap(asteroid1, width, height, false);
        asteroid2 = Bitmap.createScaledBitmap(asteroid2, width, height, false);
        asteroid3 = Bitmap.createScaledBitmap(asteroid3, width, height, false);
    }

    public Bitmap getAsteroid() {
        /*
        if (asteroidCounter == 1) {
            asteroidCounter++;
            return asteroid1;
        }

        if (asteroidCounter == 2) {
            asteroidCounter++;
            return asteroid2;
        }

        asteroidCounter = 1;
        return asteroid3;
        */
        return asteroid1;
    }

    public Rect getCollisionShape() {
        return new Rect(x, y, x + width, y + height);
    }
}