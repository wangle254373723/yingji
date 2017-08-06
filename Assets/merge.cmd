echo %cd% 
 movieMaker -f image2 -i screen_43\%%d.jpg -i bgmusic\3.mp3 -ss 00:00:00 -to 00:00:30 -vcodec libx264 -r 30 -vf vflip video\43.mp4 -y 
 pause