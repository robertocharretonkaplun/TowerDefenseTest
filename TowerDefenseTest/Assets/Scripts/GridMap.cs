using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
GridMap : MonoBehaviour {
  public bool findDistance = false;
  public GameObject[,] gridArray;
  public Vector2Int beginPosition;
  public Vector2Int endPosition;

  public List<GameObject> path = new List<GameObject>();
  private int rows = 20;
  private int columns = 20;
  private float gridSpace = 10.5f;

  [SerializeField]
  private GameObject NodeCell;

  // Start is called before the first frame update
  void
  Start() {
    GenerateGridArray();
    GenerateGrid();
  }

  // Update is called once per frame
  void
  Update() {
    if (findDistance) {
      SetDistance();
      SetPath();
      findDistance = false;
    }
  }

  /*
   * brief: Method in charge of the generation of the Grid Map
   * note: Is necessary to assign the reference of NodeCell to be able to generate
   * the map.
   */
  private void
  GenerateGrid() {
    // Check if Node(Cell) exist
    if (!NodeCell) {
      Debug.LogError("GridMap - Node(Cell) is not assigned, check NodeCell GameObject Reference.");
    }
    else
    {
      // Generate the grid
      for (int i = 0; i < columns; i++) {
        for (int j = 0; j < rows; j++) {
          GameObject cell = Instantiate(NodeCell,
                                 new Vector3(i * gridSpace, 0, j * gridSpace),
                                 Quaternion.identity);
          cell.transform.SetParent(gameObject.transform);
          cell.GetComponent<GridData>().x = i;
          cell.GetComponent<GridData>().y = j;
          // Assign the intance objects into the array
          gridArray[i, j] = cell;
          
        }
      }
    }
  }

  private void
  GenerateGridArray() {
    gridArray = new GameObject[columns, rows];

  }

  /*
   * brief: Method in charge of initialize the gridArray variable   
   */
  private void
  SetGridArray() {
    // Set all the Node(cells) in the grid as visited
    foreach (GameObject cell in gridArray) {
      cell.GetComponent<GridData>().visited = -1;
    }
    // Start the begining position as visited
    var beginNodeData = gridArray[beginPosition.x, beginPosition.y].GetComponent<GridData>();
    beginNodeData.visited = 0;
  }

  /*
   * brief: Method in charge of setting the directions that the objects will follow
   * _dir: tells which case can be use: 1 - up, 2 - right, 3 - down, 4 - left
   */
  private bool
  direction(int _x, int _y, int _step, int _dir) {
    switch (_dir) {
      case 4:
        var NodeDataLeft = gridArray[_x + 1, _y].GetComponent<GridData>();
        if (_x - 1 > -1 && gridArray[_x + 1, _y] && NodeDataLeft.visited == _step) {
          return true;
        }
        else {
          return false;
        }
      case 3:
        var NodeDataDown = gridArray[_x, _y - 1].GetComponent<GridData>();
        if (_y - 1 > -1 && gridArray[_x, _y - 1] && NodeDataDown.visited == _step) {
          return true;
        }
        else {
          return false;
        }
      case 2:
        var NodeDataRight = gridArray[_x + 1, _y].GetComponent<GridData>();
        if (_x + 1 < columns && gridArray[_x + 1, _y] && NodeDataRight.visited == _step) {
          return true;
        }
        else {
          return false;
        }
      case 1:
        var NodeDataUp = gridArray[_x, _y + 1].GetComponent<GridData>();
        if (_y + 1 < rows && gridArray[_x, _y + 1] && NodeDataUp.visited == _step) {
          return true;
        }
        else {
          return false;
        }
    }
    return false;
  }

  /*
   * brief: Method in charge of setting a NodeCell as visited or not
   */
  private void
  SetVisitedNodes(int _x, int _y, int _step) {
    if (gridArray[_x, _y]) {
      gridArray[_x, _y].GetComponent<GridData>().visited = _step;
    }
  }

  /*
   * brief: Method in charge of checking all the directions.
   */
  private void CheckAllDirections(int _x, int _y, int _step)
  {
    // Check Up Direction
    if (direction(_x, _y, -1, 1)) {
      SetVisitedNodes(_x, _y + 1, _step);
    }
    // Check Right Direction
    if (direction(_x, _y, -1, 2)) {
      SetVisitedNodes(_x + 1, _y, _step);
    }
    // Check Down Direction
    if (direction(_x, _y, -1, 3)) {
      SetVisitedNodes(_x, _y - 1, _step);
    }
    // Check Left Direction
    if (direction(_x, _y, -1, 4)) {
      SetVisitedNodes(_x - 1, _y, _step);
    }
  }

  /*
   * brief: Method in charge of validating and check is is possible to move into 
   * multiple positions.
   */
  private void
  SetDistance() {
    SetGridArray();
    int x = beginPosition.x;
    int y = beginPosition.y;
    int size = rows * columns;
    int[] validatingArray = new int[size];

    // Check all possible steps from each NodeCell
    for (int step = 1; step < size; step++) {
      foreach (GameObject cell in gridArray) {
        var cellData = cell.GetComponent<GridData>();
        if (cell && cell.GetComponent<GridData>().visited == step - 1) {
          CheckAllDirections(cell.GetComponent<GridData>().x, cell.GetComponent<GridData>().y, step);
        }
      }
    }
  }

  private void SetPath()
  {
    int step;
    List<GameObject> tmpList = new List<GameObject>();

    // Clear the path list in case it has something on it.
    path.Clear();
    // Check if the NodeCell existe and add it into our path
    var endCell = gridArray[endPosition.x, endPosition.y];
    if (endCell && endCell.GetComponent<GridData>().visited > 0) {
      // Make the traversal path
      path.Add(endCell);
      step = endCell.GetComponent<GridData>().visited - 1;
    }
    else {
      Debug.Log("End position  is not possible to reach");
      return;
    }

    // Check if we are evaluating the correct direction
    for (int i = step; step > -1; i--) {
      if (direction(endPosition.x, endPosition.y, step, 1)) {
        tmpList.Add(gridArray[endPosition.x, endPosition.y + 1]);
      }
      if (direction(endPosition.x, endPosition.y, step, 2)) {
        tmpList.Add(gridArray[endPosition.x + 1, endPosition.y]);
      }
      if (direction(endPosition.x, endPosition.y, step, 3)) {
        tmpList.Add(gridArray[endPosition.x, endPosition.y - 1]);
      }
      if (direction(endPosition.x, endPosition.y, step, 4)) {
        tmpList.Add(gridArray[endPosition.x - 1, endPosition.y]);
      }
    }

    // Return the closest node possible.
    GameObject tmpObject = FindCloseLocation(gridArray[endPosition.x, endPosition.y].transform, tmpList);
    path.Add(tmpObject);
    endPosition.x = tmpObject.GetComponent<GridData>().x;
    endPosition.y = tmpObject.GetComponent<GridData>().y;
    tmpList.Clear();
  }


  GameObject FindCloseLocation(Transform _targetLocation, List<GameObject> _list)
  {
    float currentDistance = gridSpace * rows * columns;
    int index = 0;
    for (int i = 0; i < _list.Count; i++)
    {
      if (Vector3.Distance(_targetLocation.position, _list[i].transform.position) < currentDistance)
      {
        currentDistance = Vector3.Distance(_targetLocation.position, _list[i].transform.position);
        index = i;
      }
    }
    return _list[index];
  }
}
