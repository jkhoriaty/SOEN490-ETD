package concordia_university_capstone.etd_v10;

import android.app.Service;
import android.content.Intent;
import android.location.Location;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;


public class BackgroundLocation extends Service implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener, LocationListener
{
    String deviceID;
    String serverIP;
    int serverPort;

    GoogleApiClient apiClient;
    LocationRequest locationRequest;

    public BackgroundLocation() {
    }

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        throw new UnsupportedOperationException("Not yet implemented");
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startID)
    {
        Toast.makeText(this, "Service Started", Toast.LENGTH_SHORT).show();
        return  START_STICKY;
    }

    @Override
    public void onDestroy()
    {
        super.onDestroy();

        Toast.makeText(this, "Service Stopped", Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onConnected(Bundle connectionHint)
    {
        Log.i("ConnectionTestProcess", "Connection succeeded");
    }

    @Override
    public void onConnectionFailed(ConnectionResult result)
    {
        Log.i("ConnectionTestProcess", "Connection failed: ConnectionResult.getErrorCode() = " + result.getErrorCode());
    }

    @Override
    public void onConnectionSuspended(int cause)
    {
        Log.i("ConnectionTestProcess", "Connection suspended");
        apiClient.connect();
    }

    @Override
    public void onLocationChanged(Location location)
    {
        new Thread(new LocationSenderThread(serverIP, serverPort, deviceID, location)).start();
    }
}
