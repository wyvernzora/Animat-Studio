<div style="width: 100%; margin: 20px; text-align: center;">
	<img alogn="center" src="https://github.com/jluchiji/Animat-Studio/blob/master/Animat.Studio/Resources/logo-banner-dark.png?raw=true" />
</div>

#Disk Caching

##Overview
It is fairly apparent that Animat Studio will more often than not load large amounts of information into memory. A lot of operations may also require a lot of time. Examples of such might be loading assets (*multi-frame images, large spritesheets, etc.*) and rendering complex frames. 

In order to maintain UI responsiveness, Animat Studio creates two cache files in each project directory: `cache-index.bin` and `cache-data.bin`. Uses and formats of these file will be described below in this document.

##Design Goals
It is fairly easy to implement a caching system that will generate separate files for each time-consuming operation. However, such approach may lead to tens of thousands of small cache files (*imagine a 300 frame GIF being split into separate images and every one of those cached in different sizes*). A large number of files is less error resistant that the approach with one large cache file, therefore we use the latter approach.

Another important design goal is to ensure minimum impact of cache format on the performance of the program. It is accomplished by utilizing classes in `libWyvernzora.IO` namespace that allow partial stream references.

##Cache Format
###`cache-index.bin`
`cache.bin` is the "index file", where metadata about location and nature of cached data. The `cache.bin` will begin with the following header:
	
	128	bits		: Magic number (AMTS)

Every metadata entry consists of `0x100` bytes, which are arranged in the following format

	8    bytes		: Data Address
	8	bytes		: Data Length
	240  bytes		: Entry Name
	
Entries with address of `-1` will be considered empty.

###`cache-data.bin`
Data file is simply a sequential file aligned to `0x800`.

##Operations
###Adding Cache Entry

First of all, the cache entry needs space to write its metadata. Cache manager will first search the `cache-index.bin` for an empty slot and write the metadata there. If no empty slots are found, the metadata is apended to the end of the index file. Then, cache manager looks for a hole (empty section not used by existing entries) large enough to accomodate the entry, and writes it there. If no holes are found, the entry data is appended to the end of the `cache-data.bin`.

###Removing Cache Entry

Removing an entry is as simple as overwriting the entry's address with `-1`. This way, the index will be considered invalid, making the space where metadata resides an empty slot. Without being pointed to by any metadata, section in the data file that is occupied by the removed entry is marked as a hole which will be overwritten when appropriate.

###Getting Cache Data

If the entry exists, getting cache data is as simple as constructing a `PartialStreamEx` from the start of the data to its end.