using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static int currentMaxInvaders = 10;
    public int difficultyIncrement = 5;
    public int step = 1;
    private int intialStep = 0;
    public GameObject invader;
    public Transform target;
    private GameObject[] invaders;
    private Vector3 spawnPos;
    private GUIStyle guiStyle;
    private bool gameStart = true;
    private bool levelStart = true;
    private int levelStringWidth;
    private int level = 1;
    private bool gameOver = false;

    // Use this for initialization
    void Start() {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = Screen.width / 32;
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontStyle = FontStyle.Bold;
        levelStringWidth = guiStyle.fontSize * 6;

        spawnPos = this.transform.position;
        this.intialStep = step;
    }

    //Show Message at game start
    void OnGUI() {
        useGUILayout = false;
        if (Event.current.type == EventType.Repaint) {
            if (gameStart) {
                //Left label
                GUI.Label(new Rect((Screen.width / 8), (Screen.height / 2) - 100, 200, 100), "SHOOT TO START", guiStyle);
                //Right label
                GUI.Label(new Rect((Screen.width / 2) + (Screen.width / 8), (Screen.height / 2) - 100, 200, 100), "SHOOT TO START", guiStyle);
                if (gameOver) {
                    //Left label
                    GUI.Label(new Rect((Screen.width / 8) + 50, (Screen.height / 2) - 25, 200, 100), "GAME OVER", guiStyle);
                    //Right label
                    GUI.Label(new Rect((Screen.width / 2) + (Screen.width / 8) + 50, (Screen.height / 2) - 25, 200, 100), "GAME OVER", guiStyle);
                }
            } else {
                string tmp = "Level " + level;
                //Left label
                GUI.Label(new Rect(levelStringWidth, (Screen.height / 2) - 100, 200, 100), tmp, guiStyle);
                //Right label
                GUI.Label(new Rect((Screen.width / 2) + levelStringWidth, (Screen.height / 2) - 100, 200, 100), tmp, guiStyle);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (levelStart) {
            guiStyle.normal.textColor = Color.white;
        } else {
            guiStyle.normal.textColor = Color.Lerp(guiStyle.normal.textColor, Color.clear, 0.25f * Time.deltaTime);
        }
        levelStart = false;

        if (gameStart || gameOver) {
            Time.timeScale = 0;
            if ((Input.GetKey("space") || Cardboard.SDK.Triggered)) {
                gameStart = false;
                gameOver = false;
                Time.timeScale = 1;
                invaders = new GameObject[currentMaxInvaders];
                this.spawnInvaders(currentMaxInvaders);
            }
        } else {
            //Current Level Move Invaders
            if (this.countInvaders() > 0) {
                Vector3 v1, v2, v3, v4;
                for (int i = 0; i < currentMaxInvaders; i++) {
                    if (invaders[i] != null) {
                        Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                        Vector3 pos = invaders[i].transform.position;
                        v1 = rule1(tmpRigid);
                        v2 = rule2(tmpRigid);
                        v3 = rule3(tmpRigid);
                        v4 = moveTowardsCam(tmpRigid);

                        invaders[i].transform.LookAt(target);
                        Vector3 result = v1+v2+v3+v4;
                        if (!float.IsNaN(result.x) && !float.IsNaN(result.y) && !float.IsNaN(result.z)) {
                            //tmpRigid.velocity += result;
                            //tmpRigid.position += tmpRigid.velocity;
                            if(this.getDistanceFromPlayer(tmpRigid) < 30) {
                                //If close stop being part of swarm and move towards player
                                invaders[i].transform.position = Vector3.MoveTowards(pos, new Vector3(0,0,0), 1 * step * Time.deltaTime);
                            } else {
                                //Use Swarm intelligence
                                invaders[i].transform.position = Vector3.MoveTowards(pos, result, 1 * step * Time.deltaTime);
                            }
                        }
                    }
                }
            } else {
                //New Level
                this.level++;
                if (this.step < 15) {
                    this.step++;
                }
                this.levelStart = true;
                if (currentMaxInvaders <= 40) {
                    currentMaxInvaders += difficultyIncrement;
                }
                invaders = new GameObject[currentMaxInvaders];
                this.spawnInvaders(currentMaxInvaders);
            }
        }
    }

    //Rule 1
    Vector3 rule1(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                if (!invaders[i].Equals(inv)) {
                    Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                    tmpVector += tmpRigid.position;
                }
            }
        }
        tmpVector = tmpVector / (this.countInvaders() - 1);

        return (tmpVector - inv.position) / 100;
    }

    //Rule 2
    Vector3 rule2(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                if (!invaders[i].Equals(inv)) {
                    Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                    if ((tmpRigid.position - inv.position).magnitude < 25) {
                        tmpVector -= (tmpRigid.position - inv.position);
                    }
                }
            }
        }
        return tmpVector;
    }

    //Rule 3
    Vector3 rule3(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                if (!invaders[i].Equals(inv)) {
                    Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                    tmpVector += tmpRigid.velocity;
                }
            }
        }
        tmpVector = tmpVector / (this.countInvaders() - 1);
        return (tmpVector - inv.velocity) / 8;
    }

    //Rule 4
    //Move Towards Camera
    Vector3 moveTowardsCam(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        return (tmpVector - inv.position) / 100;
    }


    public float getDistanceFromPlayer(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        float distance = (inv.position - tmpVector).magnitude;
        return distance;
    }

    //Spawn the invaders
    void spawnInvaders(int invaderCount) {
        for (int i = 0; i < invaderCount; i++) {
            Vector3 pos = makeNew();
            invaders[i] = Instantiate(invader, pos, transform.rotation) as GameObject;
            invaders[i].name = "Spawn" + i.ToString();
        }
    }

    //Counts the number of live invaders
    int countInvaders() {
        int count = 0;
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                count++;
            }
        }
        return count;
    }

    public void setGameOver() {
        this.gameOver = true;
        this.resetGame();
    }

    //Get Random Vector close to spawn point
    Vector3 makeNew() {
        float x = Random.Range(-50, 50) + this.spawnPos.x;
        float y = Random.Range(-10, 70) + this.spawnPos.y;
        //float z = UnityEngine.Random.Range(10, 60) + this.spawnPos.z;
        float z = this.spawnPos.z;
        if (x < 0) {
            x -= 10;
        } else {
            x += 10;
        }

        if (y < 0) {
            y -= 10;
        } else {
            y += 10;
        }

        if (z < 0) {
            z -= 10;
        } else {
            z += 10;
        }
        Vector3 pos = new Vector3(x, y, z);
        return pos;
    }

    private void resetGame() {
        GameObject gui = GameObject.Find("GUI");
        HealthBar bar = gui.GetComponent<HealthBar>();
        bar.lives = 3;
        this.gameStart = true;
        this.levelStart = true;
        this.level = 1;
        this.step = this.intialStep;
        currentMaxInvaders = 10;
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                Destroy(invaders[i]);
            }
        }
        invaders = new GameObject[currentMaxInvaders];
    }
}
