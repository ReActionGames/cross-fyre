using System.Collections;
using System.Collections.Generic;
using CrossFyre;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class wall
    {
        public class calculate_size
        {
            private Vector2 initialColliderSize = Vector2.zero;
            private Vector2 topRight = Vector2.one;
            private Vector2 bottomLeft = Vector2.zero;


            [Test]
            public void size_x_of_north_wall_is_same_as_viewport_x()
            {
                var expected = initialColliderSize.With(x: 1);
                var actual = Wall.CalculateSize(topRight, bottomLeft, initialColliderSize, WallsManager.Side.North);

                Assert.AreEqual(expected.x, actual.x);
            }

            [Test]
            public void size_y_of_north_wall_is_not_changed()
            {
                var expected = initialColliderSize.With(x: 1);
                var actual = Wall.CalculateSize(topRight, bottomLeft, initialColliderSize, WallsManager.Side.North);

                Assert.AreEqual(expected.y, actual.y);
            }

            [Test]
            public void size_y_of_east_wall_is_same_as_viewport_y()
            {
                var expected = initialColliderSize.With(y: 1);
                var actual = Wall.CalculateSize(topRight, bottomLeft, initialColliderSize, WallsManager.Side.East);

                Assert.AreEqual(expected.y, actual.y);
            }

            [Test]
            public void size_x_of_east_wall_is_not_changed()
            {
                var expected = initialColliderSize.With(y: 1);
                var actual = Wall.CalculateSize(topRight, bottomLeft, initialColliderSize, WallsManager.Side.East);

                Assert.AreEqual(expected.x, actual.x);
            }
        }

        public class calculate_position
        {
            private Vector2 initialColliderSize = Vector2.one;
            private Vector2 initialColliderPosition = Vector2.zero;
            private Vector2 topRight = Vector2.one;
            private Vector2 bottomLeft = Vector2.zero;


            [Test]
            public void position_x_of_north_wall_is_middle_of_viewport()
            {
                var expected = initialColliderPosition.With(x: 0.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.North);

                Assert.AreEqual(expected.x, actual.x);
            }

            [Test]
            public void position_y_of_east_wall_is_middle_of_viewport()
            {
                var expected = initialColliderPosition.With(y: 0.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.East);

                Assert.AreEqual(expected.y, actual.y);
            }

            [Test]
            public void position_y_of_north_wall_is_top_of_viewport_plus_half_of_wall_size_y()
            {
                var expected = initialColliderPosition.With(y: 1.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.North);

                Assert.AreEqual(expected.y, actual.y);
            }

            [Test]
            public void position_x_of_east_wall_is_right_of_viewport_plus_half_of_wall_size_x()
            {
                var expected = initialColliderPosition.With(x: 1.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.East);

                Assert.AreEqual(expected.x, actual.x);
            }

            [Test]
            public void position_y_of_south_wall_is_bottom_of_viewport_minus_half_of_wall_size_y()
            {
                var expected = initialColliderPosition.With(y: -0.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.South);

                Assert.AreEqual(expected.y, actual.y);
            }

            [Test]
            public void position_x_of_west_wall_is_left_of_viewport_minus_half_of_wall_size_x()
            {
                var expected = initialColliderPosition.With(x: -0.5f);
                var actual = Wall.CalculatePosition(topRight, bottomLeft, initialColliderSize, WallsManager.Side.West);

                Assert.AreEqual(expected.x, actual.x);
            }
        }
    }
}
