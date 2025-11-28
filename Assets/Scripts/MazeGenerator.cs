using JetBrains.Annotations;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 15;
    public int height = 15;

    public GameObject player;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject goalPrefab;

    private int[,] maze;

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    // 迷路をランダム生成（穴掘り法）
    void GenerateMaze()
    {
        maze = new int[width, height];

        // 全部壁で埋める
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                maze[x, y] = 1;

        // スタート地点（常に左下付近）
        int cx = 1;
        int cy = 1;
        maze[cx, cy] = 0;

        System.Random rand = new System.Random();

        // 穴掘り開始
        Dig(cx, cy, rand);
    }

    // 再帰的に穴を掘る
    void Dig(int x, int y, System.Random rand)
    {
        // ランダム方向
        int[] dirs = { 0, 1, 2, 3 };
        Shuffle(dirs, rand);

        foreach (int dir in dirs)
        {
            int dx = 0, dy = 0;

            if (dir == 0) { dx = 1; dy = 0; }
            if (dir == 1) { dx = -1; dy = 0; }
            if (dir == 2) { dx = 0; dy = 1; }
            if (dir == 3) { dx = 0; dy = -1; }

            int nx = x + dx * 2;
            int ny = y + dy * 2;

            if (IsInside(nx, ny) && maze[nx, ny] == 1)
            {
                maze[x + dx, y + dy] = 0; // １マス掘る
                maze[nx, ny] = 0;         // 次のマスも掘る
                Dig(nx, ny, rand);        // 再帰
            }
        }
    }

    bool IsInside(int x, int y)
    {
        return x > 0 && y > 0 && x < width - 1 && y < height - 1;
    }

    // 配列シャッフル
    void Shuffle(int[] array, System.Random rand)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            int tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    // 迷路を画面に配置する
    void BuildMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 床
                Instantiate(floorPrefab, new Vector3(x, 0, y), Quaternion.identity);

                if (maze[x, y] == 1)
                {
                    Instantiate(wallPrefab, new Vector3(x, 1, y), Quaternion.identity);
                }
            }
        }

        // ゴール配置（右上付近）
        Instantiate(goalPrefab, new Vector3(width - 2, 0.5f, height - 2), Quaternion.identity);

        if (player != null)
        {
            player.transform.position = new Vector3(1,1,1);
            player.transform.rotation = Quaternion.identity;
        }
    }
}
