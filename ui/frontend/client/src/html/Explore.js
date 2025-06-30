import React, { useEffect, useState } from 'react';
import '../css/explore.css';

function Explore() {
    const images = [
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2019/05/firefighters-park-troy-4-1024x576.jpg",
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2020/06/Jaycee-Park-in-Troy-1.jpg",
        "https://patch.com/img/cdn/users/489361/2011/07/raw/27832d1f093d7de2b897cd8f1deba8e8.jpg?width=1200",
    ]

    const [imgIndex, setImgIndex] = useState(0);

    useEffect(() => {
        const interval = setInterval(() => {
            setImgIndex((prevImgIndex) =>
            prevImgIndex === images.length - 1 ? 0 : prevImgIndex + 1);
        }, 5000);    //5000ms

        return () => clearInterval(interval);
    }, [images.length]);

    const nextImage = () => {
        setImgIndex(imgIndex === images.length - 1 ? 0 : imgIndex + 1);
    }

    const prevImage = () => {
        setImgIndex(imgIndex === 0 ? images.length - 1 : imgIndex - 1);
    }

    const goToImage = (index) => {
        setImgIndex(index);
    }

  return <div>
    <h1 class="title">Explore the Parks</h1>
    <div class="carousel-container">
        <img src={images[imgIndex]} alt="Image carousel of different parks in Troy."></img>
    </div>

    <div class="image-list-container">
        <img src={images[0]}></img>
        <img src={images[1]}></img>
        <img src={images[2]}></img>

    </div>
  </div>;
}

export default Explore;
