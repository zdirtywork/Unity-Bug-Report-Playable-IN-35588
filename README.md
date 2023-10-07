# [Won't Fix] Unity-Bug-Report-Playable-IN-35588

**Unity has stated that they will not fix this bug.**

> RESOLUTION NOTE:
Thank you for bringing this issue to our attention. Unfortunately, after careful consideration we will not be addressing your issue at this time, as we are currently committed to resolving other higher-priority issues, as well as delivering the new animation system. Our priority levels are determined by factors such as the severity and frequency of an issue and the number of users affected by it. However we know each case is different, so please continue to log any issues you find, as well as provide any general feedback on our roadmap page to help us prioritize.

## About this issue

When `animator.applyRootMotion` is set to `false`, calling `animator.BindStreamTransform(animator.transform)` can cause the character's transform to become abnormal.
- If the character does not implement the `OnAnimatorMove` message, the character will be fixed in the starting position and cannot be moved by setting `transform.position`.
- If the character implements the `OnAnimatorMove` message, the character can be moved by setting `transform.position`, but when connecting the animation playable, the character will be pulled back to the starting position.

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode.
3. Click the "Move Character Away" button to make the character move away from its starting position. Additionally, you can move the character away by dragging it in the Scene view.
4. Click the "Reconnect Playable" button, and you will see the character being pulled back to its starting position.
