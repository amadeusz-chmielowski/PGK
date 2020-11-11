using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator instance;

    public Transform levelStartPoint;

    public List<LevelPieceBasic> levelPrefabs = new List<LevelPieceBasic>();
    public List<LevelPieceBasic> pieces = new List<LevelPieceBasic>();
    public LevelPieceBasic finish;
    public LevelPieceBasic start;
    private bool FinishGenerated = false;
    private int GeneratedLevels = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ShowPiece((LevelPieceBasic)Instantiate(start));
        AddPiece();
        AddPiece();
    }

    private void ShowPiece(LevelPieceBasic piece)
    {
        piece.transform.SetParent(this.transform, false);
        if (pieces.Count < 1)
            piece.transform.position = new Vector2(
            levelStartPoint.position.x - piece.startPoint.localPosition.x,
            levelStartPoint.position.y - piece.startPoint.localPosition.y);
        else
            piece.transform.position = new Vector2(
            pieces[pieces.Count - 1].exitPoint.position.x - pieces[pieces.Count - 1].
            startPoint.localPosition.x,
            pieces[pieces.Count - 1].exitPoint.position.y - pieces[pieces.Count - 1].
            startPoint.localPosition.y);
        pieces.Add(piece);
        GeneratedLevels++;
    }

    // Update is called once per frame
    public void AddPiece()
    {
        if (GeneratedLevels < 5)
        {
            int randomIndex = UnityEngine.Random.Range(0, levelPrefabs.Count);
            Debug.LogError(randomIndex + " | " + Convert.ToString(levelPrefabs.Count));
            LevelPieceBasic piece = (LevelPieceBasic)Instantiate(levelPrefabs[randomIndex]);
            ShowPiece(piece);

        }
        else
        {
            if (!FinishGenerated)
            {
                LevelPieceBasic piece = (LevelPieceBasic)Instantiate(finish);
                ShowPiece(piece);
                FinishGenerated = true;
            }

        }
    }

    public void RemoveOldestPiece()
    {

        if (pieces.Count > 2 * 5)
        {
            LevelPieceBasic oldestPiece = pieces[0];
            pieces.RemoveAt(0);
            Destroy(oldestPiece.gameObject);
        }
    }
}
