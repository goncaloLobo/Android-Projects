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
    private Button dragButton;
    private LinearLayout dropButton;
    TextView tv, success, dropD;
    int total, fail = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        dragButton = findViewById(R.id.one);
        dropButton = (LinearLayout) findViewById(R.id.bottomlinear1);
        tv = findViewById(R.id.Total);
        success = findViewById(R.id.Success);
        dropD = findViewById(R.id.drop);

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
                        dropD.setBackgroundResource(R.drawable.woodenbox2);
                        total += 1;
                        int value = total - fail;
                        success.setText("Drops: " + value);
                        tv.setText("Total drops: " + total);
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

        dragButton.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                ClipData data = ClipData.newPlainText("", "");
                View.DragShadowBuilder shadow = new View.DragShadowBuilder(dragButton);
                v.startDragAndDrop(data, shadow, null, 0);
                return false;
            }
        });
    }
}
