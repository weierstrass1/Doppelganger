var ours = [];
var npar2013 = [];
var nearest = [];
var downsample = [];

for(var i = 0; i < frame; i++){
	ours[i] = new Image();
	ours[i].src = "./PixelArt/" + id + "/Ours/" + id + "_ours_" + i + ".png";
	ours[i].onload = ImageLoaded;
	
	npar2013[i] = new Image();
	npar2013[i].src = "./PixelArt/" + id + "/NPAR2013/" + id + "_npar2013_" + i + ".png";
	npar2013[i].onload = ImageLoaded;
	
	nearest[i] = new Image();
	nearest[i].src = "./PixelArt/" + id + "/Nearest/" + id + "_nearest_" + i + ".png";
	nearest[i].onload = ImageLoaded;
	
	downsample[i] = new Image();
	downsample[i].src = "./PixelArt/" + id + "/Downsample/" + id + "_downsample_" + i + ".png";
	downsample[i].onload = ImageLoaded;
}

var ours_img = document.getElementById("Ours");
var npar2013_img = document.getElementById("NPAR2013");
var nearest_img = document.getElementById("Nearest");
var downsample_img = document.getElementById("Downsample");
var frame_label = document.getElementById("current_frame");

function AssignFrame(new_index){
	ours_img.src=ours[new_index].src;
	npar2013_img.src=npar2013[new_index].src;
	nearest_img.src=nearest[new_index].src;
	downsample_img.src=downsample[new_index].src;
	frame_label.innerHTML  = new_index;
}