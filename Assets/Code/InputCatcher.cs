using Code.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code
{
    public class InputCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LayerMask shootMask;

        private Camera _camera;

        public OnDownEvent OnDownEvent { get; private set; }
        public OnDragEvent OnDragEvent { get; private set; }
        public OnUpEvent OnUpEvent { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;

            OnDownEvent = new OnDownEvent();
            OnDragEvent = new OnDragEvent();
            OnUpEvent = new OnUpEvent();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownEvent.Invoke(eventData.position);
            /*
                var ray = _camera.ScreenPointToRay(eventData.position);
                
                if (Physics.Raycast(ray, out var hit, groundMask))
                {
                    OnDownEvent.Invoke(hit.point);
                }
                else
                {
                    OnDownEvent.Invoke(ray.GetPoint(1000));
                }
            */
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpEvent.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragEvent.Invoke(eventData.position);
            
            /*
                var ray = _camera.ScreenPointToRay(eventData.position);

                if (Physics.Raycast(ray, out var hit, float.MaxValue, shootMask))
                {
                    OnDragEvent.Invoke(hit.point);
                }
                else
                {
                    OnDragEvent.Invoke(ray.GetPoint(1000));
                }
            */
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent.Invoke(eventData.position);
            
            /*
                var ray = _camera.ScreenPointToRay(eventData.position);

                if (Physics.Raycast(ray, out var hit, float.MaxValue, shootMask))
                {
                    OnDragEvent.Invoke(hit.point);
                }
                else
                {
                    OnDragEvent.Invoke(ray.GetPoint(1000));
                }
            */
        }
    }
}