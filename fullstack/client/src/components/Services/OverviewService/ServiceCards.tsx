import React from 'react'
import {useAtom} from "jotai";
import {ServicesAtom} from "../../../atoms/atoms.ts";

export const ServiceCards = () => {
    const [services] = useAtom(ServicesAtom);
    return <>
        {services.map((service) => (
            <Card key={service.id} title={service.name} description={service.description} img={service.imageUrl}/>))}
    </>;

};

const Card = ({title, description, img}: {
    title: string;
    description: string;
    img: string;
}) => {
    return <div className="col-span-4">
        <div className="flex mb-4 items-start justify-between">
            <div className="card bg-gray-800 w-96 shadow-sm pb-10">
                <div className="card-body">
                    <h2 className="card-title">{title}</h2>
                    <p>{description}</p>
                </div>
                <figure>
                    <img
                        src={img}
                        width={300}
                        height={300}
                        alt="Shoes" />
                </figure>
            </div>
        </div>
    </div>
}