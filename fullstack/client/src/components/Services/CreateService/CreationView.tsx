import React, { useState } from 'react'
import {CreateAvailabilityRoute} from "../../../helpers/routeConstants.tsx";
import {serviceClient} from "../../../apiControllerClients.ts";
import {useAtom} from "jotai";
import {CreatedServiceAtom, JwtAtom, ServicesAtom} from "../../../atoms/atoms.ts";
import {ServiceCreateRequestDto} from "../../../models/generated-client.ts";
import toast from "react-hot-toast";
import {useNavigate} from "react-router";

export const CreationView = () => {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [image, setImage] = useState("");
    const [jwt] = useAtom(JwtAtom);
    const [, setCreatedService] = useAtom(CreatedServiceAtom);
    const [services, setServices] = useAtom(ServicesAtom);
    const navigate = useNavigate();
    const [base64, setBase64] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    const createServiceDto: ServiceCreateRequestDto = {
        name: title,
        description: description,
        imageUrl: base64,
    }


    return (
        <div className="px-4">
            <div className="flex flex-col md:flex-row gap-6">
                <div className="w-full md:w-1/3 space-y-4">
                    <div>
                        <label htmlFor="title" className="block mb-2 text-sm text-text-grey">Title</label>
                        <input
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            id="title"
                            className="w-full px-4 py-2 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            placeholder="Enter title"
                        />
                    </div>

                    <div>
                        <label htmlFor="description" className="block mb-2 text-sm text-text-grey">Description</label>
                        <textarea
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            id="description"
                            rows={4}
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            placeholder="Enter description"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Upload Image</label>
                        <input
                            type="file"
                            accept="image/*"
                            onChange={(e) => {
                                const file = e.target.files?.[0];
                                setImage(file ? URL.createObjectURL(file) : "");

                                let reader = new FileReader();
                                reader.readAsDataURL(file!);

                                reader.onload = () => {
                                    if (typeof reader.result == "string") {
                                        setBase64(reader.result.split(",")[1]);
                                    }
                                };

                                reader.onerror = () => {
                                    toast.error("Error while loading image");
                                };
                            }}
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>
                </div>

                <div className="w-full md:w-2/3 flex justify-center items-start">
                    {image ? (
                        <img
                            src={image}
                            alt="Selected"
                            className="max-w-full max-h-[500px] object-cover border rounded"
                        />
                    ) : (
                        <div className="text-gray-400 text-center italic">
                            No image selected
                        </div>
                    )}
                </div>
            </div>

            <div className="absolute bottom-6 right-6">
                <button className="flex text-sm items-center gap-2 bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue] transition-colors rounded px-3 py-1.5"
                        onClick={() => {
                            setIsLoading(true);
                            serviceClient.createService(createServiceDto, jwt)
                                .then(r => {
                                    toast.success("Service created successfully");
                                    setCreatedService(r);
                                    setServices([...services, r]);
                                    navigate(CreateAvailabilityRoute);
                                })
                                .catch(() => {
                                    toast.error("Could not create service");
                                })
                                .finally(() => {
                                    setIsLoading(false);
                                });
                        }
                }>
                    <span>{isLoading ? "Creating..." : "Choose available time slots"}</span>
                </button>
            </div>
            {isLoading && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="animate-spin rounded-full h-16 w-16 border-t-4 border-b-4 border-[--color-text-baby-blue]"></div>
                </div>
            )}
        </div>
    )
}
