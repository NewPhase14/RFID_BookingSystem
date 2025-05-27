// path: components/ServiceCards.tsx

import React from 'react';
import { useAtom } from "jotai";
import { JwtAtom, ServiceAtom, ServicesAtom } from "../../../atoms/atoms.ts";
import { FiEdit, FiTrash } from "react-icons/fi";
import { serviceClient } from "../../../apiControllerClients.ts";
import toast from "react-hot-toast";
import { UpdateServiceRoute } from "../../../helpers/routeConstants.tsx";
import { useNavigate } from "react-router";

export const ServiceCards = () => {
    const [services] = useAtom(ServicesAtom);
    return (
        <>
            {services.map((service) => (
                <Card
                    key={service.id}
                    id={service.id}
                    title={service.name}
                    description={service.description}
                    img={service.imageUrl}
                />
            ))}
        </>
    );
};

const truncate = (text: string) => text.length > 20 ? text.slice(0, 20) + '...' : text;

const Card = ({
                  id,
                  title,
                  description,
                  img
              }: {
    id: string;
    title: string;
    description: string;
    img: string;
}) => {
    const [services, setServices] = useAtom(ServicesAtom);
    const [, setService] = useAtom(ServiceAtom);
    const [jwt] = useAtom(JwtAtom);
    const navigate = useNavigate();

    return (
        <div className="col-span-4">
            <div className="flex">
                <div className="card bg-gray-800 w-dvw shadow-sm pb-10">
                    <div className="card-body">
                        <div className="flex justify-between items-start">
                            <h2 className="card-title">{truncate(title)}</h2>
                            <div className="flex space-x-2">
                                <button
                                    onClick={() => {
                                        serviceClient.getServiceById(id, jwt).then((r) => {
                                            setService(r);
                                            navigate(UpdateServiceRoute);
                                        });
                                    }}
                                    className="btn btn-sm"
                                >
                                    <FiEdit />
                                </button>
                                <button
                                    onClick={() => {
                                        serviceClient.deleteService(id, jwt).then(() => {
                                            toast.success("Service deleted successfully");
                                            setServices(services.filter((service) => service.id !== id));
                                        });
                                    }}
                                    className="btn btn-sm bg-red-800"
                                >
                                    <FiTrash />
                                </button>
                            </div>
                        </div>
                        <p>{truncate(description)}</p>
                    </div>
                    <figure>
                        <img src={img} width={300} height={300} alt="service image" />
                    </figure>
                </div>
            </div>
        </div>
    );
};
