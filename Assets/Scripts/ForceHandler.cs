using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Questo componente serve a salvare le forze applicate , mostrarle tramite LineRenderer e applicarle al cambio di tempo.
 * funziona insieme a TimeHandler
 * 
 */
public class ForceHandler : MonoBehaviour
{
    //vettore forza che indica LA RISULTANTE tra tutte le forze applicate al baricentro
    private Vector3 BaricentricforceToApply;

    //WARNING
    // l'applicazione delle forze in un punto specifico funziona solo in parte
    private Vector3 PointForceToApply;
    private Vector3 PointWhereApply;

    private Rigidbody rb;


    private LineRenderer lineRenderer;
    public LineParameters lParams;//definisce lo stile della lineRenderer
    public ForceMode fm = ForceMode.Impulse;
    public float maxLineLength = 5f;

    // Start is called before the first frame update
    void Start()
    {

        //inizializzo tutti i valori
        rb = GetComponent<Rigidbody>();
        BaricentricforceToApply = new Vector3();
        PointForceToApply = new Vector3();
        PointWhereApply = transform.position;

    }
    //aggiunge una forza in direzione direction e di forza "magnitude" alle altre gia aplpicate in precedenza con il metodo +=
    //nb: ma la somma tra vettori!
    public void addBaricentricForce(Vector3 direction, float magnitude, float maxMagnitude)
    { //se il tempo e fermo
        
        if (rb.isKinematic)
        {
            //applico la forza
            
            if (BaricentricforceToApply.magnitude < maxMagnitude)
                BaricentricforceToApply += direction * magnitude;
            else
                BaricentricforceToApply = (direction+ BaricentricforceToApply.normalized).normalized * BaricentricforceToApply.magnitude;//when the value of the vectore is maximum you can change only the direction

            //e controllo se c'e gia un lineRenderer da aggiornare o devo aggiungerne uno nuovo
            lineRenderer = transform.gameObject.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                //aggiorna il vettore visivo in base alle nuove forze
                //updateForceLine(transform.position, direction);
                updateForceLine(BaricentricforceToApply.magnitude / maxMagnitude);
            }
            else
            {
                //aggiunge un nuovo vettore visivo
                addForceLine();
                Vector3 vettoreVisivo = direction * (magnitude/maxMagnitude)* maxLineLength;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + vettoreVisivo);
            }
            Debug.Log("Mod:" + BaricentricforceToApply.magnitude + " Dir:" + BaricentricforceToApply.normalized);

        }
        else
        {
            //FORBIDDEN
            Debug.Log("non puoi applicare forza quando il corpo  non e kinematik");
        }

    }
    //funzione che aggiunge una forza in un punto (da completare)
    public void addPointForce(Vector3 direction, Vector3 hitPoint, float magnitude)
    {

        //aggiorno la forza

        PointForceToApply = direction * magnitude;
        PointWhereApply = hitPoint;

        //aggiorna il line renderer

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer)
        {
            updateForceLine(magnitude);
           // updateForceLine(hitPoint, direction);
        }
        else
        {

            addForceLine();

            lineRenderer.SetPosition(0, hitPoint);
            lineRenderer.SetPosition(1, hitPoint + PointForceToApply);
        }


    }
    private void updateForceLine(float maxLenPerc)
    {
        Vector3 vettoreVisivo = BaricentricforceToApply.normalized * maxLenPerc * maxLineLength;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + vettoreVisivo);
        lineRenderer.endColor = Color.Lerp(lParams.endMinColor, lParams.endMaxColor, maxLenPerc);
    }
    /*
    private void updateForceLine(Vector3 hitPoint, Vector3 direction)
    {

        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 endPos = lineRenderer.GetPosition(1);

        lineRenderer.SetPosition(0, (hitPoint + startPos) / 2);
        lineRenderer.SetPosition(1, (hitPoint + startPos) / 2 + ((endPos - startPos).magnitude + lParams.increaseFactor) * direction);

    }*/

    //aggiunge un lineRenderer a cui bisogna poi solo settare l'inizio e la fine
    public void addForceLine()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.material = lParams.material;
        lineRenderer.useWorldSpace = lParams.useWorldSpace;
        lineRenderer.startWidth = lParams.startWidth;
        lineRenderer.endWidth = lParams.endWidth;
        lineRenderer.startColor = lParams.startColor;
        lineRenderer.endColor = lParams.endMinColor;
        lineRenderer.numCapVertices = lParams.numCapVertices;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Apply()
    {//applico prima la forza nel punto e poi quella nel baricentro
        //rb.AddForceAtPosition(PointForceToApply, PointWhereApply, ForceMode.Impulse);
        rb.AddForceAtPosition(BaricentricforceToApply, transform.position, fm);
        Debug.Log(transform.name +" "+ BaricentricforceToApply.x+" "+ BaricentricforceToApply.y + " " + BaricentricforceToApply.z + " " + transform.position.x + " " + transform.position.y + " " + transform.position.z);
        //resetto tutti i parametri e tolgo il lineRenderer
        BaricentricforceToApply = new Vector3(0, 0, 0);
        PointForceToApply = new Vector3(0, 0, 0);
        PointWhereApply = transform.position;
        Destroy(gameObject.GetComponent<LineRenderer>());
    }
}
