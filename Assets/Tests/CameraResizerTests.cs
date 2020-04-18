using CrossFyre;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class CameraResizerTests
    {
        public class calc_view_rect
        {

            [Test]
            public void camera_resizes_to_one_bounds()
            {
                var bounds = new[]
                {
                    new Bounds(Vector2.zero, Vector2.one)
                };
                
                var expected = new Rect(-.5f, .5f,.5f,-.5f);
                var actual = CameraResizer.CalcViewRectFromBounds(bounds);
                
                Assert.AreEqual(expected, actual);
            }
            
            [Test]
            public void camera_resizes_to_two_bounds()
            {
                var bounds = new[]
                {
                    new Bounds(Vector2.left/2, Vector2.one),
                    new Bounds(Vector2.right/2, Vector2.one) 
                };
                
                var expected = new Rect(-1f, .5f,1f,-.5f);
                var actual = CameraResizer.CalcViewRectFromBounds(bounds);
                
                Assert.AreEqual(expected, actual);
            }
        }

        public class add_padding
        {
            [Test]
            public void adds_1_padding_to_size_1_rect()
            {
                var rect = new Rect(Vector2.zero, Vector2.one);
                const float padding = 1f;
                
                var expected = new Rect(-1f, 2, 2, -1);
                var actual = CameraResizer.AddPadding(rect, padding);
                
                Assert.AreEqual(expected, actual);
            }
        }
    }
}