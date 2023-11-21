using System;
using UnityEngine;

public class AnimatorTester : MonoBehaviour
{
    private Animator _animator;

    // Full book open/close
    [SerializeField] private bool isFullBook;
    private static readonly int OpenBook = Animator.StringToHash("OpenBook");
    private static readonly int CloseBook = Animator.StringToHash("CloseBook");
    private static readonly int FlipPage = Animator.StringToHash("FlipPage");

    // Mini book open/close
    [SerializeField] private bool isNotebook;
    private static readonly int OpenNotebook = Animator.StringToHash("NotebookOpen");
    private static readonly int CloseNotebook = Animator.StringToHash("NotebookClose");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FullBookOpen();
            NotebookOpen();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FullBookFlip();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FullBookClose();
            NotebookClose();
        }
    }

    private void FullBookOpen()
    {
        if (!isFullBook) return;
        _animator.SetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
        _animator.ResetTrigger(CloseBook);
    }

    private void FullBookFlip()
    {
        if (!isFullBook) return;
        _animator.SetTrigger(FlipPage);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(CloseBook);
    }

    private void FullBookClose()
    {
        if (!isFullBook) return;
        _animator.SetTrigger(CloseBook);
        _animator.ResetTrigger(OpenBook);
        _animator.ResetTrigger(FlipPage);
    }

    private void NotebookOpen()
    {
        if (!isNotebook) return;
        _animator.SetTrigger(OpenNotebook);
        _animator.ResetTrigger(CloseNotebook);
    }

    private void NotebookClose()
    {
        if (!isNotebook) return;
        _animator.SetTrigger(CloseNotebook);
        _animator.ResetTrigger(OpenNotebook);
    }
}