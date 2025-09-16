/*
Correctly handle PNG transparency in Win IE 5.5 & 6.
http://homepage.ntlworld.com/bobosola. Updated 18-Jan-2006.
------------------
PNG fix change Log
------------------
18-Jan-2006:
  New version of pngfix.js and pngfix_map.js (special version for imagemaps)
  with new method of calling these files. The way to use the script now is:
  
  <!--[if lt IE 7]>
  <script defer type="text/javascript" src="pngfix.js"></script>
  <![endif]-->

- Ready for IE 7. Conditional comments changed to run with IE browsers less
  than v7 with sniffer code within that to only run on 5.5 or higher.
  This was done to better protect non-IE browsers against misuse by bad 
  copy/pasting where the script is wrongly run without the conditional comments.

- Added test for (document.body.filters) to ensure only IE would run
  the code. This again to protect non-IE browsers where the script is misused 
  as above (issue reported by Opera Software).

- Added use of DEFER keyword in script tag and removed
  window.attachEvent("onload", correctPNG) from pngfix.js. This removes the
  ugly on-load flicker sometimes seen with multiples of images. The DEFER 
  keyword ensures the filtering is done before the images are rendered and
  runs after any existing body onload code. See Dean Edwards' blog at 
  http://dean.edwards.name/weblog/2005/09/busted/
*/

// The inline PNG fix, courtesy of Bob Osola, http://homepage.ntlworld.com/bobosola/
var arVersion = navigator.appVersion.split("MSIE")
var version = parseFloat(arVersion[1])

if ((version >= 5.5) && (document.body.filters)) 
{
   for(var i=0; i<document.images.length; i++)
   {
      var img = document.images[i]
      var imgName = img.src.toUpperCase()
      if (imgName.substring(imgName.length-3, imgName.length) == "PNG")
      {
         var imgID = (img.id) ? "id='" + img.id + "' " : ""
         var imgClass = (img.className) ? "class='" + img.className + "' " : ""
         var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
         var imgStyle = "display:inline-block;" + img.style.cssText 
         if (img.align == "left") imgStyle = "float:left;" + imgStyle
         if (img.align == "right") imgStyle = "float:right;" + imgStyle
         if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
         var strNewHTML = "<span " + imgID + imgClass + imgTitle
         + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
         + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
         + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>" 
		 
         img.outerHTML = strNewHTML
         i = i-1
      }
   }
}
