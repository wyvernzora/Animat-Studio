<div style="width: 100%; margin: 20px; text-align: center;">
	<img alogn="center" src="https://github.com/jluchiji/AmiMat/raw/master/Documentation/Images/logo-banner-dark.png" />
</div>

##Asset Cache
A lot of assets processed by AniMat Studio may be very large in size (*spritesheets*) or involve multiple animation frames (*GIF images*). In order to ensure program stability and responsiveness, it is important to have disk cache for all image frames and thumbnails.

It is relatively easy to implement a system that caches images as individual files. However, such a system generates thousands of images in a cache directory, making it vulnerable to accidental editing, which in turn slows down the entire caching system. Therefore it is a goal to develop an archived cache with minimum impact on cache performance.

###Design Fundamentals
The cache will be split into 2 files: `cache.aci` and `cache.acd`, where `*.aci` file is the cache index and `*.acd` file is actual cache data. Data will be written-in-place, meaning that there will be no such operation as *save*. However, it may be necessary to perform such saves for the index file, therefore its separation from the bulky data portion. 

Upon import each asset will be assigned an id (which, in hexadecimal, is also the filename). Cache will be indexed by a 64-bit identifier: `[32-bit asset id] [1-bit type id] [31-bit frame index]`.

###Implementation Details
The `*.aci` index file will consist entirely of cache entry data. Each entry will be 16 bytes wide, with the following format:

	8 bytes : Cache Entry ID
	4 bytes : Cache Entry Address
	4 bytes : Cache Entry Length

Cache entries with ID of `0x0000000000000000` will be considered empty and skipped during loading. The data file will be aligned to `0x800`, and empty spaces between entries will be considered **holes**. When a new cache entry is pushed to the cache, it should first attemp to place it in a hole large enough to accomodate it, then, if no such hole is found, append the entry to the end of the data file. In case of excessive total hole size (i.e. fragmentation), cache data file should be defragmentated in the background without user knowledge. 
 