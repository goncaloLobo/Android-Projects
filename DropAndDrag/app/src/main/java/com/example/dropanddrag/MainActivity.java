package com.example.dropanddrag;

import android.app.Activity;
import android.content.ClipData;
import android.os.Bundle;
import android.view.DragEvent;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

public class MainActivity extends Activity {
    private Button dragButton1, dragButton2, dragButton3, dragButton4, dragButton5, dragButton6;
    private LinearLayout dropButton;
    TextView tv, success, dropD1, dropD2, dropD3, dropD4, dropD5, dropD6;
    private int total, fail = 0, id = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        dragButton1 = findViewById(R.id.box1);
        dragButton2 = findViewById(R.id.box2);
        dragButton3 = findViewById(R.id.box3);
        dragButton4 = findViewById(R.id.box4);
        dragButton5 = findViewById(R.id.box5);
        dragButton6 = findViewById(R.id.box6);
        dropButton = (LinearLayout) findViewById(R.id.bottomlinear1);
        //tv = findViewById(R.id.Total);
        //success = findViewById(R.id.Success);

        dropD1 = findViewById(R.id.drop1);
        dropD2 = findViewById(R.id.drop2);
        dropD3 = findViewById(R.id.drop3);
        dropD4 = findViewById(R.id.drop4);
        dropD5 = findViewById(R.id.drop5);
        dropD6 = findViewById(R.id.drop6);

        dropButton.setOnDragListener(new View.OnDragListener() {
            @Override
            public boolean onDrag(View v, DragEvent event) {
                final int action = event.getAction();

                switch (action) {
                    case DragEvent.ACTION_DRAG_STARTED:
                        break;
                    case DragEvent.ACTION_DRAG_EXITED:
                        break;
                    case DragEvent.ACTION_DRAG_ENTERED:
                        break;
                    case DragEvent.ACTION_DROP: {
                        fail += 1;
                        return true;
                    }
                    case DragEvent.ACTION_DRAG_ENDED: {
                        // verificar se é dentro dos parametros
                        switch (id) {
                            case 1:
                                dropD1.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            case 2:
                                dropD2.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            case 3:
                                dropD3.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            case 4:
                                dropD4.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            case 5:
                                dropD5.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            case 6:
                                dropD6.setBackgroundResource(R.drawable.woodenbox2);
                                break;
                            default:
                                break;
                        }

                        total += 1;
                        int value = total - fail;
                        //success.setText("Drops: " + value);
                        //tv.setText("Total drops: " + total);
                        return true;
                    }
                    case DragEvent.ACTION_DRAG_LOCATION:
                        int xValue = (int) event.getX();
                        int yValue = (int) event.getY();
                        //Log.d("values", "x: " + xValue + " y: " + yValue);
                    default:
                        break;
                }
                return true;
            }
        });

        dragButton1.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton1);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 1;
                return false;
            }
        });

        dragButton2.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton2);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 2;
                return false;
            }
        });

        dragButton3.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton3);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 3;
                return false;
            }
        });

        dragButton4.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton4);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 4;
                return false;
            }
        });

        dragButton5.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton5);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 5;
                return false;
            }
        });

        dragButton6.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton6);
                v.startDragAndDrop(data, shadow, null, 0);
                id = 6;
                return false;
            }
        });
    }
}
