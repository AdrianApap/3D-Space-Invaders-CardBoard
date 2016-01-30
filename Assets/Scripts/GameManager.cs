using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    //Attributes
    //GUI Attributes
    private GUIStyle guiStyle;
    private int levelStringWidth;

    //Game Attributes
    private List<GameObject> invaders;
    public int currentMaxInvaders = 10;
    public int difficultyIncrement = 5;
    public int step = 1;
    private int intialStep = 0;
    public GameObject invader;
    public Transform target;
    private Vector3 spawnPos;

    //Level Attributes
    private int level = 1;
    private bool gameStart = true;
    private bool levelStart = true;
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
                invaders = new List<GameObject>(currentMaxInvaders);
                this.spawnInvaders();
            }
        } else {
            this.removeAllNulls();

            //Current Level Move Invaders
            if (this.invaders.Count > 0) {
                if (this.invaders.Count > 1) {
                    for (int i = 0; i < this.invaders.Count; i++) {
                        if (invaders[i] != null) {
                            Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                            Vector3 currentPos = invaders[i].transform.position;
                            Vector3 goalPos = this.getSwarmPos(tmpRigid);
                            invaders[i].transform.LookAt(target);

                            if (this.getDistanceFromPlayer(tmpRigid) < 40) {
                                //If close stop being part of swarm and move towards player
                                tmpRigid.velocity += goalPos;
                                //invaders[i].transform.position = Vector3.MoveTowards(currentPos, new Vector3(0, 0, 0), 1 * step * Time.deltaTime);
                            } else {
                                //Use Swarm intelligence
                                invaders[i].transform.position = Vector3.MoveTowards(currentPos, goalPos, 1 * step * Time.deltaTime);
                            }

                        }
                    }
                } else {
                    invaders[0].transform.position = Vector3.MoveTowards(invaders[0].transform.position, new Vector3(0, 0, 0), 1 * step * Time.deltaTime);
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
                invaders = new List<GameObject>(currentMaxInvaders);
                this.spawnInvaders();
            }
        }
    }

    //Get Swarm Pos
    Vector3 getSwarmPos(Rigidbody inv) {
        Vector3 vector1 = new Vector3(0, 0, 0);
        Vector3 vector2 = new Vector3(0, 0, 0);
        Vector3 vector3 = new Vector3(0, 0, 0);
        Vector3 vector4 = new Vector3(0, 0, 0);

        for (int i = 0; i < this.invaders.Count; i++) {
            if (invaders[i] != null) {
                if (!invaders[i].Equals(inv)) {
                    //Rule 1
                    Rigidbody tmpRigid = invaders[i].GetComponent<Rigidbody>();
                    vector1 += tmpRigid.position;

                    //Rule 2
                    if ((tmpRigid.position - inv.position).magnitude < 25) {
                        vector2 -= (tmpRigid.position - inv.position);
                    }

                    //Rule 3
                    vector3 += tmpRigid.velocity;
                }
            }
        }

        int currentInvaders = this.invaders.Count;

        //Rule 1
        vector1 = (vector1 / (currentInvaders - 1));
        vector1 = (vector1 - inv.position) / 100;

        //Rule 2

        //Rule 3
        vector3 = vector3 / (currentInvaders - 1);
        vector3 = (vector3 - inv.velocity) / 8;

        //Rule 4
        vector4 = (vector4 - inv.position) / 100;

        return (vector1 + vector2 + vector3 + vector4);
    }


    float getDistanceFromPlayer(Rigidbody inv) {
        Vector3 tmpVector = new Vector3(0, 0, 0);
        float distance = (inv.position - tmpVector).magnitude;
        return distance;
    }

    //Spawn the invaders
    void spawnInvaders() {
        for (int i = 0; i < this.currentMaxInvaders; i++) {
            Vector3 pos = makeNew();
            GameObject tmpInvader = Instantiate(invader, pos, transform.rotation) as GameObject;
            tmpInvader.name = "Spawn" + i.ToString();
            this.invaders.Add(tmpInvader);
        }
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

    void removeAllNulls() {
        this.invaders.RemoveAll(item => item == null);
    }

    void resetGame() {
        GameObject gui = GameObject.Find("GUI");
        HealthBar bar = gui.GetComponent<HealthBar>();
        bar.lives = 3;
        this.gameStart = true;
        this.levelStart = true;
        this.level = 1;
        this.step = this.intialStep;
        for (int i = 0; i < currentMaxInvaders; i++) {
            if (invaders[i] != null) {
                Destroy(invaders[i]);
            }
        }
        currentMaxInvaders = 10;
        invaders = new List<GameObject>(currentMaxInvaders);
    }
}

