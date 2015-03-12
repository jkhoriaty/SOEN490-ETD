package concordia_university_capstone.etd_v10;

import android.app.Service;
import android.content.Intent;
import android.location.Location;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;
import android.widget.CheckBox;
import android.widget.Toast;
import android.view.View;

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
    public void onCreate()
    {
        buildGoogleApiClient();

        createLocationRequest();

        apiClient.connect();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startID)
    {
        deviceID = intent.getStringExtra("deviceID");
        serverIP = intent.getStringExtra("serverIP");
        serverPort = intent.getIntExtra("serverPort", -1);

        if (apiClient.isConnected())
        {
            LocationServices.FusedLocationApi.requestLocationUpdates(apiClient, locationRequest, this);
        }
        return  START_STICKY;
    }

    @Override
    public void onDestroy()
    {
        super.onDestroy();
        LocationServices.FusedLocationApi.removeLocationUpdates(apiClient, this);
        if(apiClient.isConnected())
        {
            apiClient.disconnect();
        }


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

    protected synchronized void buildGoogleApiClient()
    {
        apiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();
    }

    protected void createLocationRequest()
    {
        locationRequest = new LocationRequest();
        locationRequest.setInterval(10000);
        locationRequest.setFastestInterval(1000);
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
    }

    public void startLocationUpdates(View view)
    {
        LocationServices.FusedLocationApi.requestLocationUpdates(apiClient, locationRequest, this);
    }

    public void stopLocationUpdates(View view)
    {
        LocationServices.FusedLocationApi.removeLocationUpdates(apiClient, this);

    }
}
