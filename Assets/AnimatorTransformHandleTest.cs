using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

// About this issue:
// 
// When `animator.applyRootMotion` is set to `false`, calling `animator.BindStreamTransform(animator.transform)` can
// cause the character's transform to become abnormal.
// - If the character does not implement the `OnAnimatorMove` message,
//   the character will be fixed in the starting position and cannot be moved by setting `transform.position`.
// - If the character implements the `OnAnimatorMove` message, the character can be moved by setting `transform.position`,
//   but when connecting the animation playable, the character will be pulled back to the starting position.
//
// How to reproduce:
// 
// 1. Open the "SampleScene".
// 2. Enter play mode.
// 3. Click the "Move Character Away" button to make the character move away from its starting position.
//    Additionally, you can move the character away by dragging it in the Scene view.
// 4. Click the "Reconnect Playable" button, and you will see the character being pulled back to its starting position.

[RequireComponent(typeof(Animator))]
public class AnimatorTransformHandleTest : MonoBehaviour
{
    public AnimationClip clip;

    private Animator _animator;
    private PlayableGraph _graph;
    private AnimationMixerPlayable _amp;
    private AnimationClipPlayable _acp;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = false; // IMPORTANT
        _animator.BindStreamTransform(_animator.transform); // IMPORTANT

        _graph = PlayableGraph.Create("Animator Transform Handle Test");
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        _acp = AnimationClipPlayable.Create(_graph, clip);
        _amp = AnimationMixerPlayable.Create(_graph, 2);
        _amp.ConnectInput(0, _acp, 0, 1f);

        var output = AnimationPlayableOutput.Create(_graph, "Anim Output", _animator);
        output.SetSourcePlayable(_amp);

        _graph.Play();
    }

    private void OnDestroy()
    {
        _graph.Destroy();
    }

    // Without this method, the character will be stick at its starting position.
    // You can't move it away by setting its transform.position.
    // ReSharper disable once Unity.RedundantEventFunction
    private void OnAnimatorMove() // IMPORTANT
    {
        // Nothing to do here.
    }

    public void MoveCharacterAway()
    {
        _animator.transform.position = new Vector3(1, -1, 0);
    }

    public void ReconnectPlayable()
    {
        _amp.DisconnectInput(0);
        _amp.ConnectInput(0, _acp, 0, 1f); // IMPORTANT
    }
}