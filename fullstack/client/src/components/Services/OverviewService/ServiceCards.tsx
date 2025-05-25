import React from 'react'
import {useAtom} from "jotai";
import {ServicesAtom} from "../../../atoms/atoms.ts";
import {FiTrash} from "react-icons/fi";
import {serviceClient} from "../../../apiControllerClients.ts";
import toast from "react-hot-toast";

export const ServiceCards = () => {
    const [services] = useAtom(ServicesAtom);
    return <>
        {services.map((service) => (
            <Card key={service.id} id={service.id} title={service.name} description={service.description} img={service.imageUrl}/>))}
    </>;

};

const Card = ({id, title, description, img}: {
    id: string;
    title: string;
    description: string;
    img: string;
}) => {
    const [services, setServices] = useAtom(ServicesAtom);

    return (
        <div className="col-span-4">
            <div className="flex">
                <div className="card bg-gray-800 w-dvw shadow-sm pb-10">
                    <div className="card-body">
                        <div className="flex justify-between items-start">
                            <h2 className="card-title">{title}</h2>
                            <div className="flex space-x-2">
                                <button className="btn btn-sm">edit</button>
                                <button onClick={() => {
                                    serviceClient.deleteService(id).then(() => {
                                        toast.success("Service deleted successfully");
                                        setServices(services.filter(service => service.id !== id));
                                    })
                                }} className="btn btn-sm bg-red-800"><FiTrash/></button>
                            </div>
                        </div>
                        <p>{description}</p>
                    </div>
                    <figure>
                        <img
                            src={img}
                            width={300}
                            height={300}
                            alt="Shoes"
                        />
                    </figure>
                </div>
            </div>
        </div>
    );
}