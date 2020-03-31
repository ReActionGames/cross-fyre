using CrossFyre.GameInput;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class InputTests
    {
        public class calc_origin_point
        {
            private const float Threshold = 5;

            [Test]
            public void origin_doesnt_move_if_distance_below_threshold()
            {
                var origin = Vector2.zero;
                var touch = Vector2.right * 3;

                var expected = origin;
                var actual = VirtualJoystickInput.CalcOriginPoint(origin, touch, Threshold);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void origin_moves_to_correct_point_if_outside_threshold_to_the_right()
            {
                var origin = Vector2.zero;
                var touch = Vector2.right * 10;

                var expected = Vector2.right * 5;
                var actual = VirtualJoystickInput.CalcOriginPoint(origin, touch, Threshold);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void origin_moves_to_correct_point_if_outside_threshold_to_the_lower_left()
            {
                var origin = Vector2.zero;
                var touch = new Vector2(-10f, -10f);

                var expected = new Vector2(-6.464f, -6.464f);
                var actual = VirtualJoystickInput.CalcOriginPoint(origin, touch, Threshold);

                Assert.IsTrue(Vector2.Distance(expected, actual) < 0.001f);
            }

            [Test]
            public void origin_moves_to_correct_point_if_outside_threshold_when_both_not_zero()
            {
                var origin = Vector2.up * 10;
                var touch = Vector2.left * 7;
                
                var expected = new Vector2(-4.133f, 4.096f);
                var actual = VirtualJoystickInput.CalcOriginPoint(origin, touch, Threshold);
                
                Assert.IsTrue(Vector2.Distance(expected, actual) < 0.001f);
            }
        }
    }
}