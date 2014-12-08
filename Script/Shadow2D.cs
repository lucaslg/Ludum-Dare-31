using UnityEngine;
using System.Collections;

public class Shadow2D : MonoBehaviour
{
    private bool CastShadow = true;

	private GameObject m_shadowSprite;
	public Material m_shadowMaterialPrefab;
	private Material m__shadowMaterial;
    public Light m_light = null;
	private Light m__molotov = null;
	
	protected void Start ()
	{
        m_light = GameObject.Find("Lighting/FireLight").GetComponent<Light>();

	    if (CastShadow)
	    {
            // get the object on which you need to apply your shadow
            m_shadowSprite = (GameObject)Instantiate(gameObject);
            Destroy(m_shadowSprite.GetComponent<Shadow2D>());
            m_shadowSprite.transform.parent = transform;
            m_shadowSprite.transform.localPosition = Vector3.zero;
            m_shadowSprite.transform.localScale = new Vector3(1f, 2f, 1f);
            m_shadowSprite.renderer.material = (Material)Instantiate(m_shadowMaterialPrefab);

            // Shader settings
            m__shadowMaterial = m_shadowSprite.renderer.material;
            m__shadowMaterial.SetVector("_Position", new Vector3(
                transform.position.x,
                -transform.position.y,
                transform.position.z
            ));
            if (m_light)
            {
                m__shadowMaterial.SetVector("_LightPos", new Vector3(
                    m_light.transform.position.x,
                    -m_light.transform.position.y,
                    m_light.transform.position.z
                ));
                m__shadowMaterial.SetFloat("_LightAtt", m_light.range / 10f);
            }

            //transform.localPosition = 
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (CastShadow)
	    {
            // Update position
            m__shadowMaterial.SetVector("_Position", new Vector3(
                transform.position.x,
                -transform.position.y,
                transform.position.z
            ));

            // If molotov, override other lights
            if (m__molotov)
            {
                m__shadowMaterial.SetVector("_LightPos", new Vector3(
                    m__molotov.transform.position.x,
                    -m__molotov.transform.position.y,
                    -m__molotov.transform.position.z
                ));
                return;
            }

            // If dynamic light
            if (!m_light)
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                float nearest = float.PositiveInfinity;
                int nearestInd = -1;
                for (int i = 0; i < lights.Length; ++i)
                {
                    float dist = Vector3.Distance(lights[i].transform.position, transform.position);
                    if (Vector3.Distance(lights[i].transform.position, transform.position) < nearest)
                    {
                        nearest = dist;
                        nearestInd = i;
                    }
                }
                m__shadowMaterial.SetVector("_LightPos", new Vector3(
                    lights[nearestInd].transform.position.x,
                    -lights[nearestInd].transform.position.y,
                    -lights[nearestInd].transform.position.z
                ));
                m__shadowMaterial.SetFloat("_LightAtt", lights[nearestInd].range / 10f);
            }
	    }
	}

	public void EnableMolotov(Light m)
	{
		m__molotov = m;
	}
	public void DisableMolotov()
	{
		m__molotov = null;
	}
}
