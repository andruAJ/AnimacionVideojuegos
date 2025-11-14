using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MergeMeshes : MonoBehaviour
{
    void Start()
    {
        var combineList = new List<CombineInstance>();

        Matrix4x4 parentWorldToLocal = transform.worldToLocalMatrix;

        foreach (Transform child in transform)
        {
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            if (meshFilter == null || meshFilter.sharedMesh == null)
                continue;

            CombineInstance ci = new CombineInstance();
            ci.mesh = meshFilter.sharedMesh;
            // Convert child's world matrix into parent's local space so vertices keep their world position/rotation relative to parent
            ci.transform = parentWorldToLocal * child.localToWorldMatrix;
            combineList.Add(ci);

            // Desactivar el hijo para no duplicar la geometría en escena
            child.gameObject.SetActive(false);
        }

        if (combineList.Count == 0) return;

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.CombineMeshes(combineList.ToArray(), true, true);

        var mf = GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;
        gameObject.SetActive(true);
    }
}
