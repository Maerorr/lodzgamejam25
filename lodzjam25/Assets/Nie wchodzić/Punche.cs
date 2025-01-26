using UnityEngine;
using System.Collections;
public class Punche : MonoBehaviour
{
   [Header("Billboard Settings")]
    public Texture2D[] textures; // Użytkownik podaje tekstury
    public GameObject billboardPrefab; // Prefab billboarda z przypisanym MeshRendererem
    public float billboardLifetime = 3f; // Jak długo bilboard ma być widoczny
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 0.1f); // Początkowy rozmiar
    public Vector3 finalScale = new Vector3(1f, 1f, 1f); // Końcowy rozmiar

    [Header("Spawn Position Ranges")]
    public float minX = -1f; // Minimalna pozycja X
    public float maxX = 1f; // Maksymalna pozycja X
    public float minY = 1f; // Minimalna pozycja Y
    public float maxY = 2f; // Maksymalna pozycja Y
    public Material material;
    /// <summary>
    /// Tworzy billboard na scenie z losową teksturą i pozycją.
    /// </summary>
    public void SpawnBillboard(Vector3 startPosition)
    {
        if (textures.Length == 0 || billboardPrefab == null)
        {
            Debug.LogWarning("Brak tekstur lub prefabu billboarda!");
            return;
        }

        // Losuj teksturę
        Texture2D selectedTexture = textures[Random.Range(0, textures.Length)];

        // Losuj pozycję spawnu w podanych granicach
        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX) + transform.position.x, // Losuj X w zakresie
            Random.Range(minY, maxY) + transform.position.y, // Losuj Y w zakresie
            transform.position.z // Zostaw Z jako domyślne
        );

        // Utwórz obiekt billboarda
        GameObject billboard = Instantiate(billboardPrefab, spawnPosition, Quaternion.identity);

        // Ustaw teksturę w materiale
        MeshRenderer renderer = billboard.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            material.mainTexture = selectedTexture;
        }
        renderer.material = material;
        // Ustaw początkowy rozmiar i rozpocznij interpolację
        billboard.transform.localScale = initialScale;
        StartCoroutine(ScaleBillboard(billboard, initialScale, finalScale,startPosition,startPosition+spawnPosition ,billboardLifetime));
    }

    private IEnumerator ScaleBillboard(GameObject billboard, Vector3 startScale, Vector3 endScale,Vector3 startPos, Vector3 endPos ,float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Interpolacja liniowa rozmiaru
            billboard.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            billboard.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
        

            yield return null;
        }

        // Usuń billboard po zakończeniu
        Destroy(billboard);
    }
}
