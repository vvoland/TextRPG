namespace TextRPG
{
	public struct Rect
	{
		public int X
		{
			get
			{
				return this._XMin;
			}
			set
			{
				this._XMin = value;
			}
		}

		public int Y
		{
			get
			{
				return this._YMin;
			}
			set
			{
				this._YMin = value;
			}
		}

		public Vector2 Position
		{
			get
			{
				return new Vector2(this._XMin, this._YMin);
			}
			set
			{
				this._XMin = value.X;
				this._YMin = value.Y;
			}
		}

		public Vector2 Center
		{
			get
			{
				return new Vector2((int)(this.X + this._Width / 2.0f), (int)(this.Y + this._Height / 2.0f));
			}
			set
			{
				this._XMin = (int)(value.X - this._Width / 2.0f);
				this._YMin = (int)(value.Y - this._Height / 2.0f);
			}
		}

		public Vector2 Min
		{
			get
			{
				return new Vector2(this.XMin, this.YMin);
			}
			set
			{
				this.XMin = value.X;
				this.YMin = value.Y;
			}
		}

		public Vector2 Max
		{
			get
			{
				return new Vector2(this.XMax, this.YMax);
			}
			set
			{
				this.XMax = value.X;
				this.YMax = value.Y;
			}
		}

		public int Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				this._Width = value;
			}
		}

		public int Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				this._Height = value;
			}
		}

		public Vector2 Size
		{
			get
			{
				return new Vector2(this._Width, this._Height);
			}
			set
			{
				this._Width = value.X;
				this._Height = value.Y;
			}
		}

		public int XMin
		{
			get
			{
				return this._XMin;
			}
			set
			{
				int xMax = this.XMax;
				this._XMin = value;
				this._Width = xMax - this._XMin;
			}
		}

		public int YMin
		{
			get
			{
				return this._YMin;
			}
			set
			{
				int yMax = this.YMax;
				this._YMin = value;
				this._Height = yMax - this._YMin;
			}
		}

		public int XMax
		{
			get
			{
				return this._Width + this._XMin;
			}
			set
			{
				this._Width = value - this._XMin;
			}
		}

		public int YMax
		{
			get
			{
				return this._Height + this._YMin;
			}
			set
			{
				this._Height = value - this._YMin;
			}
		}

		private int _XMin, _YMin;

		private int _Width, _Height;

		public Rect(int x, int y, int width, int height)
		{
			this._XMin = x;
			this._YMin = y;
			this._Width = width;
			this._Height = height;
		}

		public Rect(Vector2 position, Vector2 size)
		{
			this._XMin = position.X;
			this._YMin = position.Y;
			this._Width = size.X;
			this._Height = size.Y;
		}

		public Rect(Rect source)
		{
			this._XMin = source._XMin;
			this._YMin = source._YMin;
			this._Width = source._Width;
			this._Height = source._Height;
		}

		public void Set(int x, int y, int width, int height)
		{
			this._XMin = x;
			this._YMin = y;
			this._Width = width;
			this._Height = height;
		}

		public bool Contains(Vector2 point)
		{
			return point.X >= this.XMin && point.X < this.XMax && point.Y >= this.YMin && point.Y < this.YMax;
		}

		public bool Overlaps(Rect other)
		{
			return other.XMax > this.XMin && other.XMin < this.XMax && other.YMax > this.YMin && other.YMin < this.YMax;
		}

		public static bool operator !=(Rect left, Rect right)
		{
			return !(left == right);
		}

		public static bool operator ==(Rect left, Rect right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Width.GetHashCode() ^ this.Y.GetHashCode() ^ this.Height.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if (!(other is Rect))
				return false;

			Rect rect = (Rect)other;
			return (this.X.Equals(rect.X) && this.Y.Equals(rect.Y) && this.Width.Equals(rect.Width) && this.Height.Equals(rect.Height));
		}

	}
}
