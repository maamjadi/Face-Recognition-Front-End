using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Pages
{
    /// <summary>
    /// A scrollable collection view holding many relativelayouts. The ItemsPerRow, Paddings(ColumnSpacingSides,ColumnSpacingCenter,RowSpacing), and cell height(CollectionHeight) can all be set. Items are a list of RelativeLayouts that need to be set to display collection cells.
    /// </summary>
    public class CollectionView: RelativeLayout
    {
        public List<RelativeLayout> Items = new List<RelativeLayout> { };

        /// <summary>
        /// Action that is invoked when a collection is touched.
        /// </summary>
        public Action<int> ItemTouchedAction = new Action<int>(ItemTouchedPlaceholder);

        /// <summary>
        /// The height of one collection cell.
        /// </summary>
        public int CollectionHeight = 100;
        /// <summary>
        /// The spacing in pixels between two collection cells.
        /// </summary>
        public int ColumnSpacingCenter = 20;
        /// <summary>
        /// The spacing in pixels on either side of the entire matrix of cells.
        /// </summary>
        public int ColumnSpacingSides = 20;
        /// <summary>
        /// The number of collections cells per row.
        /// </summary>
        public int ItemsPerRow = 2;
        /// <summary>
        /// The spacing in pixels in between two rows of cells (on top or on bottom).
        /// </summary>
        public int RowSpacing = 20;

        /// <summary>
        /// Used to get the entire width of the parent so the cells can be sized. Must wait till OnSizeAlloc() is called.
        /// </summary>
        private double ParentWidth = -1;

        /// <summary>
        /// Gets the pixel width of one collection cell ajusted for spacing.
        /// </summary>
        private Double CollectionWidth
        {
            get
            {
                return (this.Bounds.Width - ((ColumnSpacingCenter * (ItemsPerRow - 1)) + (ColumnSpacingSides * 2))) / ItemsPerRow;
            }
        }

        /// <summary>
        /// The layout to be placed into the ScrollView and contain all the collection cells.
        /// </summary>
        private RelativeLayout CollectionLayout;

        /// <summary>
        /// The ScrollView that will hold all the collection cells
        /// </summary>
        private MR.Gestures.ScrollView ScrollView;

        /// <summary>
        /// The position on the screen where the finger went down in the scrollview. Need this to make sure a collection isn't clicked after lifting from scrolling.
        /// </summary>
        private Point DownPoint;
        /// <summary>
        /// How many pixels one would consider enough to consider the action a scrolling intent.
        /// </summary>
        private double totalpixelsTooCancelClick = 3;
        /// <summary>
        /// If the finger is scrolling this will change to true.
        /// </summary>
        private bool hasMovedTooMuch = false;

        public CollectionView()
        {
            ScrollView = new MR.Gestures.ScrollView()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = this.Height,
                WidthRequest = this.Width,
            };
            CollectionLayout = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
            };
            this.Children.Add(
                ScrollView,
                Constraint.RelativeToParent((parent) => {
                    return (parent.Width * 0);
                }),
                Constraint.RelativeToParent((parent) => {
                    return (parent.Height * 0);
                }),
                Constraint.RelativeToParent((parent) => {
                    return (parent.Width * 1);
                }),
                Constraint.RelativeToParent((parent) => {
                    return (parent.Height * 1);
                })
            );

            //This has to be called from the constructor(or at least before OnSizeAlloc() ) or it won't allow you to scroll.
            //////////////////////////////////////////////////////////////////////////
            ScrollView.Content = CollectionLayout;
            ScrollView.Down += (object sender, MR.Gestures.DownUpEventArgs e) => {
                hasMovedTooMuch = false;
                //Using the first finger. Not trying to get into multi touch.
                DownPoint = e.Touches[0];
            };
            ScrollView.Panning += (object sender, MR.Gestures.PanEventArgs e) => {
                try
                {
                    Point point = e.Touches[0];
                    var x = point.X;
                    var y = point.Y;
                    //Figure out if the finger has moved enough to consider this a scroll.
                    if (Math.Abs((DownPoint.X - x)) > totalpixelsTooCancelClick || Math.Abs((DownPoint.Y - y)) > totalpixelsTooCancelClick)
                    {
                        hasMovedTooMuch = true;
                    }
                }
                catch
                {
                }
            };
            ScrollView.Up += (object sender, MR.Gestures.DownUpEventArgs e) => {
                //The finger came up see if this is a touch intent. Will invoke the action if so.
                CalculateItemClicked(e.Touches[0]);
            };
            //////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// Creates all the cells and places them into the ScrollView.
        /// </summary>
        public void CreateLayout()
        {
            //Index needs to start at 1 for math simplicity.
            int index = 1;
            foreach (var item in Items)
            {
                int row = GetRowFromIndex(index);
                int column = GetColumnFromIndex(index);
                CollectionLayout.Children.Add(
                    item,
                    Constraint.RelativeToParent((parent) => {
                        return GetXPosition(column);
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return GetYPosition(row);
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return (Double)CollectionWidth;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return (Double)CollectionHeight;
                    })
                );
                System.Diagnostics.Debug.WriteLine("X " + GetXPosition(column) + " Y " + GetYPosition(row) + " Row " + row + " Column " + column + " Width " + CollectionWidth);

                index++;
            }
        }
        /// <summary>
        /// Gets the X position of any specific collection cell based on the column it's in.
        /// </summary>
        private Double GetXPosition(int column)
        {
            return (0 + ColumnSpacingSides + (ColumnSpacingCenter * (column - 1)) + CollectionWidth * (column - 1));
        }
        /// <summary>
        /// Gets the Y position of any specific collection cell based on the row it's in.
        /// </summary>
        private Double GetYPosition(int row)
        {
            return (RowSpacing * row + (CollectionHeight * (row - 1)));
        }
        /// <summary>
        /// Gets the column number of a collection cell from the cell's index(starts at 1).
        /// </summary>
        private int GetColumnFromIndex(int index)
        {
            if (index <= ItemsPerRow)
            {
                return index;
            }
            else
            {
                if ((index % ItemsPerRow) != 0)
                {
                    return index % ItemsPerRow;
                }
                else
                {
                    return ItemsPerRow;
                }
            }
        }
        /// <summary>
        /// Gets the row number of a collection cell from the cell's index(starts at 1).
        /// </summary>
        private int GetRowFromIndex(int index)
        {
            return Convert.ToInt32(Math.Ceiling((Double)((Double)index / (Double)ItemsPerRow)));
        }
        /// <summary>
        /// This will calculate if the finger lifted off the screen on a collection item. If so it will invoke the action.
        /// </summary>
        private void CalculateItemClicked(Point liftedPoint)
        {
            if (!hasMovedTooMuch)
            {
                int index = 1;
                foreach (var item in Items)
                {
                    int row = GetRowFromIndex(index);
                    int column = GetColumnFromIndex(index);
                    double xPosition = GetXPosition(column);
                    double yPosition = GetYPosition(row);
                    if ((liftedPoint.X > xPosition) && (liftedPoint.X < (xPosition + CollectionWidth)) && (liftedPoint.Y > yPosition) && (liftedPoint.Y < (yPosition + CollectionHeight)))
                    {
                        //Subtract one so it is the same index as the list.
                        ItemTouchedAction.Invoke(index - 1);
                    }
                    index++;
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// A message if the action isn't registered.
        /// </summary>
        private static void ItemTouchedPlaceholder(int index)
        {
            System.Diagnostics.Debug.WriteLine("You forgot to add the action. Index " + index.ToString() + " was clicked.");
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != ParentWidth)
            {
                ParentWidth = width;
                //Create the layout now that the correct width is set.
                CreateLayout();
            }
        }
    }
}