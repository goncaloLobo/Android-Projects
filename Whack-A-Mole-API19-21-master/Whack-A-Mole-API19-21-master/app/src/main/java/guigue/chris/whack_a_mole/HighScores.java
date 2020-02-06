package guigue.chris.whack_a_mole;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import java.util.List;

public class HighScores extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_high_scores);

        ListView scoreList = (ListView)findViewById(R.id.lvScores);
        DatabaseHandler db = new DatabaseHandler(this);
        List<String> scores = db.getScores();
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, scores);
        scoreList.setAdapter(adapter);

    }
}
