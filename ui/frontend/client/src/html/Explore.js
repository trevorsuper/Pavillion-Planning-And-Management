import React, { useEffect, useState } from 'react';
import '../css/explore.css';

function Explore() {
    const images = [
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2019/05/firefighters-park-troy-4-1024x576.jpg",
        "https://metrodetroitmommy.com/familywp/wp-content/uploads/2020/06/Jaycee-Park-in-Troy-1.jpg",
        "https://patch.com/img/cdn/users/489361/2011/07/raw/27832d1f093d7de2b897cd8f1deba8e8.jpg?width=1200",
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
                <img 
                    src={images[0]} 
                    alt="Park view 1"
                    onClick={() => openModal(images[0])}
                />
                <img 
                    src={images[1]} 
                    alt="Park view 2"
                    onClick={() => openModal(images[1])}
                />
                <img 
                    src={images[2]} 
                    alt="Park view 3"
                    onClick={() => openModal(images[2])}
                />
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