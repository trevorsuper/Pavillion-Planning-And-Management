// Explore.js
import React, { useEffect, useState } from 'react';
import '../css/explore.css';

function Explore() {
    const images = [
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2019/05/firefighters-park-troy-4-1024x576.jpg",
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2020/06/Jaycee-Park-in-Troy-1.jpg",
        "https://patch.com/img/cdn/users/489361/2011/07/raw/27832d1f093d7de2b897cd8f1deba8e8.jpg?width=1200",
        "https://cdn.oaklandcountymoms.com/wp-content/uploads/2023/11/10115542/BoulanParkTroyHeader1-620x350.jpg",
        "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEiYyWeL1GgFSKrcoCjPLzMRLkx5um0cvUcGyUbgkuKuVR2oOq927kQstRq5TmwkVMxOpA7i8mVjgJSUFpz4H7EF_afhB8ALrCCADI1WwMuDuv8X3Z_6HBhWjoTNUraGtTIcl-2_i63D039j/s320/milverton+park+gifted+to+Palmerston+north+by+milvertons.JPG",
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2018/05/Untitled6.gif",
        "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fres.cloudinary.com%2Fdgbcm65u4%2Fimage%2Fupload%2Fc_fill%2Ce_sharpen%3A100%2Cg_auto%2Ch_585%2Cw_1069%2Flkevultljxyvh5nhthyr&f=1&nofb=1&ipt=2ab141f7b055988719466d5d50496fa6743e02fa71cf33383668c0b9eff7a2c9",
    ]

    const [imgIndex, setImgIndex] = useState(0);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [modalImageSrc, setModalImageSrc] = useState('');

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

    const openModal = (imageSrc) => {
        setModalImageSrc(imageSrc);
        setIsModalOpen(true);
    }

    const closeModal = () => {
        setIsModalOpen(false);
        setModalImageSrc('');
    }

    // Close modal when clicking outside the image
    const handleModalClick = (e) => {
        if (e.target.className === 'modal-overlay') {
            closeModal();
        }
    }

    // Close modal with Escape key
    useEffect(() => {
        const handleEscapeKey = (e) => {
            if (e.key === 'Escape' && isModalOpen) {
                closeModal();
            }
        }

        document.addEventListener('keydown', handleEscapeKey);
        return () => document.removeEventListener('keydown', handleEscapeKey);
    }, [isModalOpen]);

    return (
        <div>
            <h1 className="title">Explore the Parks</h1>
            <div className="carousel-container">
                <img src={images[imgIndex]} alt="Image carousel of different parks in Troy." />
            </div>

            <div className="image-list-container">
                <img src={images[0]} alt="Picture of Firefighters Park." onClick={() => openModal(images[0])}/>
                <img src={images[1]} alt="Picture of Jaycee Park." onClick={() => openModal(images[1])}/>
                <img src={images[2]} alt="Picture of Brinston Park." onClick={() => openModal(images[2])}/>
                <img src={images[3]} alt="Picture of Boulan Park." onClick={() => openModal(images[3])}/>
                <img src={images[4]} alt="Picture of Milverton Park." onClick={() => openModal(images[4])}/>
                <img src={images[5]} alt="Picture of Raintree Park." onClick={() => openModal(images[5])}/>
                <img src={images[6]} alt="Picture of Jeanne M. Stine Community Park." onClick={() => openModal(images[6])}/>
            </div>

            {isModalOpen && (
                <div className="modal-overlay" onClick={handleModalClick}>
                    <div className="modal-content">
                        <button className="modal-close" onClick={closeModal}>×</button>
                        <img src={modalImageSrc} alt="Full size park view" />
                    </div>
                </div>
            )}
        </div>
    );
}

export default Explore;