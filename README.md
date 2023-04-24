# Unity-Bug-Report-Playable-IN-35588

## About this issue

When `animator.applyRootMotion` is set to `false`, calling `animator.BindStreamTransform(animator.transform)` can cause the character's transform to become abnormal.
- If the character does not implement the `OnAnimatorMove` message, the character will be fixed in the starting position and cannot be moved by setting `transform.position`.
- If the character implements the `OnAnimatorMove` message, the character can be moved by setting `transform.position`, but when connecting the animation playable, the character will be pulled back to the starting position.

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode.
3. Click the "Move Character Away" button to make the character move away from its starting position. Additionally, you can move the character away by dragging it in the Scene view.
4. Click the "Reconnect Playable" button, and you will see the character being pulled back to its starting position.
