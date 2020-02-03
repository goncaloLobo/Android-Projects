package com.example.android.simplegesturesapp;

import android.content.Context;

public class Circle {
    int radius;

    public Circle(Context context, int radius){
        this.radius = radius;
    }

    public int getRadius(){
        return this.radius;
    }

    public double calculatePerimeter(){
        return 2*Math.PI*radius;
    }

    public double calculateArea(){
        return Math.pow(radius, 2)*Math.PI;
    }
}
