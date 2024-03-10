using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.Game2
{
    public class LineDrawer : MonoBehaviour
    {
        public GameObject linePrefab;
        private GameObject _currentLine;
        private LineRenderer _lineRenderer;
        private List<Vector2> _touchPositions = new List<Vector2>();
        [SerializeField] private Ball ball;
        [SerializeField] private float maxDistance;
        [SerializeField] private bool isDraw;
        [SerializeField] private bool isDrawing;

        // A constant value to set the sensitivity threshold for creating a new line segment.
        private const float SensitivityThreshold = 0.3f;

        private void Update()
        {
            if (isDraw) return;
            // Check if mouse button 0 (left mouse button) is pressed down.
            if (Input.GetMouseButtonDown(0))
            {
                // Call the HandleClickDown method.
                HandleClickDown();
            }
            // Check if mouse button 0 (left mouse button) is held down.
            else if (Input.GetMouseButton(0))
            {
                // Call the HandleMoving method.
                HandleMoving();
            }
            // Check if mouse button 0 (left mouse button) is released.
            else if (Input.GetMouseButtonUp(0))
            {
                // Call the HandleClickUp method.
                HandleClickUp();
            }
        }

        private void HandleMoving()
        {
            if (!isDrawing) return;

            // Get the current mouse position in world space.
            Vector2 tempTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if a new line segment can be created.
            if (CanCreateNewLine(tempTouchPos))
            {
                // Call the DrawNewLine method with the new touch position.
                DrawNewLine(tempTouchPos);
            }
        }

        private void HandleClickDown()
        {
            var startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(startPosition, transform.position) > maxDistance) return;
            // Instantiate a new line game object using the line prefab.
            _currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);

            // Get the LineRenderer component of the current line game object.
            _lineRenderer = _currentLine.GetComponent<LineRenderer>();


            // Add the current mouse position in world space to the touch positions list.
            _touchPositions.Add(startPosition);
            _touchPositions.Add(startPosition);

            // Set the start and end positions of the LineRenderer component.
            _lineRenderer.SetPosition(0, _touchPositions[0]);
            _lineRenderer.SetPosition(1, _touchPositions[1]);

            isDrawing = true;
        }

        private void DrawNewLine(Vector2 newFingerPos)
        {
            // add the new position of the finger to the list of touch positions
            _touchPositions.Add(newFingerPos);

            // increase the number of positions in the LineRenderer component
            _lineRenderer.positionCount++;

            // set the new position to the last position in the LineRenderer component
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newFingerPos);
        }

        private void HandleClickUp()
        {
            if (isDraw) return;
            if (!isDrawing) return;

            ball.Move(_touchPositions);

            // Clear the touch positions list
            _touchPositions.Clear();
            isDraw = true;
        }

        private bool CanCreateNewLine(Vector2 tempTouchPos)
        {
            if (isDraw) return false;
            // Check if the distance between the current touch position and the last touch position in the list is greater than the sensitivity threshold
            return Vector2.Distance(tempTouchPos, _touchPositions[_touchPositions.Count - 1]) > SensitivityThreshold;
        }
    }
}