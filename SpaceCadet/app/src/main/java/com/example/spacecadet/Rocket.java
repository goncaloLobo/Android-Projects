package com.example.spacecadet;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;
import android.util.Log;

import static com.example.spacecadet.GameView.screenRatioX;
import static com.example.spacecadet.GameView.screenRatioY;

public class Rocket {

    int x = 0, y = 0, width, height;
    Bitmap rocket;
    private GameView gameView;
    boolean isGoingLeft = false;
    boolean isGoingRight = false;
    boolean isGoingUp = false;
    boolean isGoingDown = false;

    public Rocket(GameView gameView, int screenX, int screenY, Resources res) {
        this.gameView = gameView;

        rocket = BitmapFactory.decodeResource(res, R.drawable.rocket);

        width = rocket.getWidth();
        height = rocket.getHeight();

        width /= 6;
        height /= 5;

        width = (int) (width * screenRatioX);
        height = (int) (height * screenRatioY);

        rocket = Bitmap.createScaledBitmap(rocket, width, height, false);

        x = screenX / 2;
        y = screenY / 2;
    }

    public Bitmap getRocket() {
        return rocket;
    }

    public Rect getCollisionShape() {
        return new Rect(x, y, x + width, y + height);
    }
}
