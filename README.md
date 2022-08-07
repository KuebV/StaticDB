![image](Images/1659838570.png)
# StaticDB
A Method of storing strings inside of images that may resemble static

## Why
I don't know, I was bored and I though it would be cool. And it kinda is

## How
Using Bitmaps and .NET the application is able to split the string into indivivdual decimals and put those strings to colors

## Usage

### Encoding:

**Input:**
```
StaticDB -encode mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien et
```

**Output:**
```
Data Squared:
Un-Optimized: 8
Optimized: 9
Image will be saved as 9x9
Image has been saved as 1659838963.png
```

### Decoding:

**Input**
```
StaticDB -decode 1659838963.png
```

**Output:**
```
Decoded String:
mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien et
```


# Build it yourself
