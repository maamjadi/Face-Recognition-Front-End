﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using FaceRecognitionFrontEnd.Models;

namespace FaceRecognitionFrontEnd.Views
{
    public partial class GridView : Grid
    {
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(GridView), null);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(GridView), null);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(Type), typeof(GridView), null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GridView)
                    , null, BindingMode.OneWay, null, (bindable, oldValue, newValue) => { ((GridView)bindable).BuildTiles(newValue as IEnumerable<object>); });

        private int _maxColumns = 2;
        private float _tileHeight = 0;

        public Type ItemTemplate
        {
            get { return (System.Type)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public GridView()
        {

            for (var i = 0; i < MaxColumns; i++)
                ColumnDefinitions.Add(new ColumnDefinition());
        }


        public int MaxColumns
        {
            get { return _maxColumns; }
            set { _maxColumns = value; }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public void BuildTiles(IEnumerable<object> tiles)
        {
            try
            {
                if (tiles == null || tiles.Count() == 0)
                    Children?.Clear();

                // Wipe out the previous row definitions if they're there.
                RowDefinitions?.Clear();

                var enumerable = tiles as IList ?? tiles.ToList();
                var numberOfRows = Math.Ceiling(enumerable.Count / (float)MaxColumns);

                for (var i = 0; i < numberOfRows; i++)
                    RowDefinitions?.Add(new RowDefinition { Height = TileHeight });

                for (var index = 0; index < enumerable.Count; index++)
                {
                    var column = index % MaxColumns;
                    var row = (int)Math.Floor(index / (float)MaxColumns);

                    var tile = BuildTile(enumerable[index]);

                    Children?.Add(tile, column, row);
                }
            }
            catch
            { // can throw exceptions if binding upon disposal
            }
        }

        private Layout BuildTile(object item1)
        {
            var buildTile = (Layout)Activator.CreateInstance(ItemTemplate, item1);
            buildTile.InputTransparent = false;

            var tapGestureRecognizer = new TapGestureRecognizer
            {
                CommandParameter = item1,
                NumberOfTapsRequired = 1,
                Command = new Command(new Action<object>(target))
            };

            buildTile.GestureRecognizers.Add(tapGestureRecognizer);


            return buildTile;
        }

        private void target(object arg2)
        {
            Command.Execute(arg2);
        }
    }
}
