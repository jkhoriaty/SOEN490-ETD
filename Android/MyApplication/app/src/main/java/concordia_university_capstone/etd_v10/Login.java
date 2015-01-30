package concordia_university_capstone.etd_v10;

import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;


public class Login extends ActionBarActivity implements UDPCallbacks
{
	String serverIP = "24.202.7.147";
	int serverPort = 2000;

    private EditText name = null;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        name = (EditText) findViewById(R.id.editText1);
    }

    public void login(View view)
    {
	    //Validate the name before sending request to the server
        if (name.getText().toString().equals("Greg"))
        {
	        //Initiate handshake with server
	        (new Thread(new ServerHandshakeThread(this, serverIP, serverPort, name.getText().toString()))).start();
        }
        else //Validation failed
        {
            Toast.makeText(getApplicationContext(), "Wrong Credentials!\nPlease type your name only.",Toast.LENGTH_SHORT).show();
        }
    }
	boolean success = false;

	public void HandshakeSucceeded(String senderID)
	{
		this.runOnUiThread(new Runnable()
		{
			@Override
			public void run()
			{
				Toast.makeText(getApplicationContext(), "Connection to server successful!",Toast.LENGTH_SHORT).show();
			}
		});
		Intent intent = new Intent(this, LocationTransmission.class);
		intent.putExtra("senderID", senderID);
		intent.putExtra("serverIP", serverIP);
		intent.putExtra("serverPort", serverPort);
		startActivity(intent);
	}

	public void HandshakeFailed()
	{
		if(success == false)
		{
			this.runOnUiThread(new Runnable()
			{
				@Override
				public void run()
				{
					Toast.makeText(getApplicationContext(), "Connection to server failed!\nPlease try again later.", Toast.LENGTH_SHORT).show();
				}
			});
		}
	}
}
