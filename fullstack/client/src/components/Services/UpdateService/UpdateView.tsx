import React, { useState } from 'react'
import { ServiceRoute} from "../../../helpers/routeConstants.tsx";
import { serviceClient } from "../../../apiControllerClients.ts";
import { useAtom } from "jotai";
import { CreatedServiceAtom, JwtAtom, ServiceAtom, ServicesAtom } from "../../../atoms/atoms.ts";
import { ServiceUpdateRequestDto } from "../../../models/generated-client.ts";
import toast from "react-hot-toast";
import { useNavigate } from "react-router";

export const UpdateView = () => {
    const [jwt] = useAtom(JwtAtom);
    const [service] = useAtom(ServiceAtom);
    const [services, setServices] = useAtom(ServicesAtom);
    const [title, setTitle] = useState(service!.name);
    const [description, setDescription] = useState(service!.description);
    const [image, setImage] = useState(service!.imageUrl);
    const [, setUpdatedService] = useAtom(CreatedServiceAtom);
    const navigate = useNavigate();
    const [base64, setBase64] = useState("");
    const [isLoading, setIsLoading] = useState(false); // <-- NEW

    const updateServiceDto: ServiceUpdateRequestDto = {
        id: service!.id,
        name: title,
        description: description,
        imageUrl: base64,
        publicId: service!.publicId
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
                <button
                    disabled={isLoading}
                    className={`flex text-sm items-center gap-2 rounded px-3 py-1.5 transition-colors
                        ${isLoading ? 'bg-gray-600 cursor-not-allowed' : 'bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue]'}`}
                    onClick={() => {
                        setIsLoading(true);
                        serviceClient.updateService(updateServiceDto, jwt).then(r => {
                            toast.success("Service updated successfully");
                            const updatedServices = services.filter((s => s.id !== r.id));
                            setUpdatedService(r);
                            setServices([...updatedServices, r]);
                            navigate(ServiceRoute);
                        }).catch(() => {
                            toast.error("Could not update service");
                        }).finally(() => {
                            setIsLoading(false);
                        });
                    }}
                >
                    <span>{isLoading ? "Updating..." : "Update service"}</span>
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
