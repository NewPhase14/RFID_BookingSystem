import React from 'react'
import logo from "../../assets/logo.png"

export const RoomCards = () => {
    return <>
        <Card
            title="Greenscreen Room"
            description="Good for making videos"
            img={logo}
        />
        <Card
            title="Studio A"
            description="Good for recording"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
        <Card
            title="3D printer"
            description="Bring out your creative side"
            img={logo}
        />
    </>;

};

const Card = ({title, description, img}: {
    title: string;
    description: string;
    img: string;
}) => {
    return <div className="col-span-4">
        <div className="flex mb-4 items-start justify-between">
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">{title}</h2>
                    <p>{description}</p>
                </div>
                <figure>
                    <img
                        src={img}
                        alt="Shoes" />
                </figure>
            </div>
        </div>
    </div>
}